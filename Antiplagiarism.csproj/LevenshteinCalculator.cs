using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using DocumentTokens = System.Collections.Generic.List<string>;

namespace Antiplagiarism
{
    public class LevenshteinCalculator
    {
        public List<ComparisonResult> CompareDocumentsPairwise(List<DocumentTokens> documents)
        {
            var res = new List<ComparisonResult>();
            for (int i = 0; i < documents.Count; i++)
                for (int j = i+1; j < documents.Count; j++)
                    res.Add(new ComparisonResult(documents[i], 
                                                 documents[j], 
                                                 GetDocDistance(documents[i], documents[j])));
            return res;
        }

        public double GetDocDistance(DocumentTokens first, DocumentTokens second)
        {
            var length = new[] { first.Count + 1, second.Count + 1 };
            var opt = new double[length[0], length[1]];
            for (int i = 0; i != length[0]; i++) 
                opt[i, 0] = i;
            for (int i = 0; i != length[1]; i++) 
                opt[0, i] = i;
            for (int i = 1; i <= first.Count; i++)
                for(int j = 1; j <= second.Count; j++)
                {
                    var distance = TokenDistanceCalculator.GetTokenDistance(first[i - 1], second[j - 1]);
                    if (distance == 0)
                        opt[i, j] = opt[i - 1, j - 1];
                    else
                        opt[i, j] = new[]{
                            opt[i , j - 1]+1,
                            opt[i - 1, j ]+1,
                            opt[i - 1, j - 1 ] + distance
                        }.Min();
                }
            return opt[first.Count, second.Count];
        }
    }
}
