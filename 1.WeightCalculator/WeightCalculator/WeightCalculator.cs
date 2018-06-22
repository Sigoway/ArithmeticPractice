using System.Collections.Generic;
using System.Linq;

namespace WeightCalculator
{
    internal class WeightCalculator
    {
        /// <summary>
        /// 获取可以称出1-maxWeight中的任一重量值的砝码组合
        /// </summary>
        /// <returns></returns>
        public static (int weight1, int weight2, int weight3) FindBestWeightGroup(int maxWeight = 13)
        {
            var weightGroup = GetWeightGroup(maxWeight);

            var weight = weightGroup.FirstOrDefault(w =>
            {
                bool result = true;
                for (int sum = 1; sum <= 13; sum++)
                {
                    if (!(result = Calculate(sum, w[0], w[1], w[2])))
                    {
                        break;
                    }
                }

                return result;
            });

            return weight == null ? (0, 0, 0) : (weight[0], weight[1], weight[2]);
        }

        /// <summary>
        /// 获取任意三个砝码相加等于sum的砝码组合
        /// </summary>
        /// <param name="sum">和</param>
        /// <returns>任意三个砝码相加等于sum的砝码组合</returns>
        private static IList<int[]> GetWeightGroup(int sum)
        {
            var lstWeight = new List<int[]>();
            var comparer = new ArrayComparer();

            for (int w1 = 1; w1 <= sum; w1++)
            {
                for (int w2 = 1; w2 <= sum; w2++)
                {
                    for (int w3 = 1; w3 <= sum; w3++)
                    {
                        var wg = new int[] { w1, w2, w3 };
                        if (sum != w1 + w2 + w3)
                        {
                            continue;
                        }

                        if (!lstWeight.Contains<int[]>(wg, comparer))
                        {
                            lstWeight.Add(wg);
                        }
                    }
                }
            }

            return lstWeight;
        }

        /// <summary>
        /// 计算使用w1，w2，w3任意的组合是否能求得sum
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="w1"></param>
        /// <param name="w2"></param>
        /// <param name="w3"></param>
        /// <returns></returns>
        private static bool Calculate(int sum, int w1, int w2, int w3)
        {
            //一个砝码
            if (sum == w1 || sum == w2 || sum == w3)
            {
                return true;
            }

            //两个砝码(sum = w1 + w2)
            if (Calculate1EqualTo2(sum, w1, w2)
                || Calculate1EqualTo2(sum, w1, w3)
                || Calculate1EqualTo2(sum, w2, w3))
            {
                return true;
            }

            //三个砝码(sum = w1 + w2 + w3)
            if (Calculate1EqualTo3(sum, w1, w2, w3)
            || Calculate1EqualTo3(w1, sum, w2, w3)
            || Calculate1EqualTo3(w2, w1, sum, w3)
            || Calculate1EqualTo3(w3, w1, w2, sum))
            {
                return true;
            }

            //三个砝码(sum + w1 = w2 + w3)
            if (Calculate2EqualTo2(sum, w1, w2, w3))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 计算w1+w2是否等于sum
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="w1"></param>
        /// <param name="w2"></param>
        /// <param name="breakCalculate">中断计算Flag</param>
        /// <returns></returns>
        private static bool Calculate1EqualTo2(int sum, int w1, int w2, bool breakCalculate = false)
        {
            if (sum == w1 + w2)
            {
                return true;
            }

            if (breakCalculate)
            {
                return false;
            }

            if (Calculate1EqualTo2(w1, sum, w2, true)
                || Calculate1EqualTo2(w2, sum, w1, true))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算w1+w2+w3是否等于sum
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="w1"></param>
        /// <param name="w2"></param>
        /// <param name="w3"></param>
        /// <param name="breakCalculate">中断计算Flag</param>
        /// <returns></returns>
        private static bool Calculate1EqualTo3(int sum, int w1, int w2, int w3, bool breakCalculate = false)
        {
            if (sum == w1 + w2 + w3)
            {
                return true;
            }

            if (breakCalculate)
            {
                return false;
            }

            if (Calculate1EqualTo3(w1, sum, w2, w3, true)
                || Calculate1EqualTo3(w2, sum, w1, w3, true)
                || Calculate1EqualTo3(w3, sum, w1, w2, true))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 计算sum+w1是否等于w2+w3
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="w1"></param>
        /// <param name="w2"></param>
        /// <param name="w3"></param>
        /// <param name="breakCalculate">中断计算Flag</param>
        /// <returns></returns>
        private static bool Calculate2EqualTo2(int sum, int w1, int w2, int w3, bool breakCalculate = false)
        {
            if (sum + w1 == w2 + w3)
            {
                return true;
            }

            if (breakCalculate)
            {
                return false;
            }

            if (Calculate2EqualTo2(sum, w2, w1, w3, true)
                || Calculate2EqualTo2(sum, w3, w1, w2, true))
            {
                return true;
            }

            return false;
        }
    }
}
