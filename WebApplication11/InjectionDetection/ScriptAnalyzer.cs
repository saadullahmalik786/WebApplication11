using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication11.InjectionDetection
{
    public static class ScriptAnalyzer
    {
        private static readonly List<string> MaliciousPatterns = new List<string>()
        {
            "select *", "drop table", "<script>", "rm -f", "--", ";--", "' or '1'='1"
        };

        public static bool IsMalicious(string input, out List<string> detectedPatterns)
        {
            detectedPatterns = MaliciousPatterns
                .Where(p => input.ToLower().Contains(p))
                .ToList();

            return detectedPatterns.Count > 0;
        }
    }
}
