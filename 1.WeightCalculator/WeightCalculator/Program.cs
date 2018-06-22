using System;

namespace WeightCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string maxWeight;
            do
            {
                Console.Write("请输入最大称重值：");
                maxWeight = Console.ReadLine();

                if (maxWeight == "q")
                {
                    break;
                }

                if (!int.TryParse(maxWeight, out int nMaxWeight))
                {
                    Console.WriteLine($"您输入的{maxWeight}不合法，请重新输入！");
                    continue;
                }

                var (weight1, weight2, weight3) = WeightCalculator.FindBestWeightGroup(nMaxWeight);

                if (weight1 == 0 && weight2 == 0 && weight3 == 0)
                {
                    Console.WriteLine($"没有找到可以称出1-{nMaxWeight}中的任一重量值的砝码组合！");

                    continue;
                }
                Console.WriteLine($"使用砝码组合({weight1},{weight2},{weight3})可以称出1-{nMaxWeight}中的任一重量值");
            } while (maxWeight != "q");
        }
    }
}
