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
            var comparisonResults = new List<ComparisonResult>();
            for (int i = 0; i != documents.Count; i++)
                for (int j = i + 1; j != documents.Count; j++)
                {
                    var result = new ComparisonResult(documents[i],documents[j],
                        GetDifference(documents[i], documents[j]));
                    comparisonResults.Add(result);
                }
            return comparisonResults;
        }

        private double GetDifference(DocumentTokens word1, DocumentTokens word2)
        {
            var opt = new double[word1.Count+1, word2.Count+1];
            for (int i = 0; i <= word1.Count; i++) 
                opt[i, 0] = i;
            for (int j = 0; j <= word2.Count; j++) 
                opt[0, j] = j;
            for (int i = 1; i <= word1.Count; i++)
                for(int j = 1; j <= word2.Count; j++)
                    opt[i, j] = Math.Min(opt[i-1,j-1]+
                                         TokenDistanceCalculator.GetTokenDistance(word1[i - 1], word2[j - 1]),
                        Math.Min(opt[i-1,j]+1,opt[i,j-1]+1));
            return opt[word1.Count, word2.Count];
        }
    }
}
