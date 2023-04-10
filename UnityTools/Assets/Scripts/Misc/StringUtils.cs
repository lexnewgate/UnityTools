using System;

namespace Lex.UnityTools
{
    public static class StringUtils
    {
        /// <summary>
        /// https://en.wikipedia.org/wiki/Levenshtein_distance
        /// LevenshteinDistance
        /// 可以用来计算两个字符串的相似度,如模糊匹配
        /// 返回值越大,相似度越低
        ///
        /// 一个简单的相似度计算方法:
        /// sim(a,b)= 1- dist(a,b) / max(a.Length, b.Length)
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static unsafe int EditDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;

            if (n == 0)
                return m;

            if (m == 0)
                return n;

            int xSize = n + 1;
            int ySize = m + 1;
            //d里面存储的总是最佳的编辑距离
            int* d = stackalloc int[xSize * ySize];

            for (int x = 0; x <= n; x++)
                d[ySize * x] = x;
            for (int y = 0; y <= m; y++)
                d[y] = y;

            for (int y = 1; y <= m; y++)
            {
                for (int x = 1; x <= n; x++)
                {
                    if (s[x - 1] == t[y - 1])
                        d[ySize * x + y] = d[ySize * (x - 1) + y - 1];  // no operation
                    else
                        d[ySize * x + y] = Math.Min(Math.Min(
                                d[ySize * (x - 1) + y] + 1,             // a deletion
                                d[ySize * x + y - 1] + 1),             // an insertion
                            d[ySize * (x - 1) + y - 1] + 1 // a substitution
                        );
                }
            }
            return d[ySize * n + m];
        }
    }
}