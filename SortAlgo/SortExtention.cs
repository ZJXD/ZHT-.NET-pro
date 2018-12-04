using System;
using System.Collections.Generic;
using System.Text;

namespace SortAlgo
{
    public static class SortExtention
    {
        #region 冒泡排序

        /*
         * 已知一组无序数据a[1]、a[2]、……a[n]，需将其按升序排列。首先比较a[1]与a[2]的值，若a[1]大于a[2]则交换两者的值，否则不变。
         * 再比较a[1]与a[3]的值，若a[1]大于a[3]则交换两者的值，否则不变。再比较a[1]与a[4]，以此类推，最后比较a[1]与a[n]的值。
         * 这样处理一轮后，a[1]的值一定是这组数据中最小的。再对a[2]~a[n]以相同方法处理一轮，则a[2]的值一定是a[2]~a[n]中最小的。
         * 降序排列与升序排列相类似，若a[1]小于a[2]则交换两者的值，否则不变，后面以此类推。 
         * 总的来讲，每一轮排序后最大（或最小）的数将移动到数据序列的最后，理论上总共要进行n(n-1）/2次交换。
         * 优点：稳定
         * 时间复杂度：理想情况下（数组本来就是有序的），此时最好的时间复杂度为o(n),最坏的时间复杂度(数据反序的)，此时的时间复杂度为o(n*n) 。
         * 冒泡排序的平均时间负责度为o(n*n).
         * 缺点：慢，每次只移动相邻的两个元素。
         */

        public static void BubbleSort(this IList<int> nubs)
        {
            int temp;
            for (int i = 0; i < nubs.Count; i++)
            {
                for (int j = i + 1; j < nubs.Count; j++)
                {
                    if (nubs[j] < nubs[i])
                    {
                        temp = nubs[j];
                        nubs[j] = nubs[i];
                        nubs[i] = temp;
                    }
                }
            }
        }

        #endregion

        #region 快速排序
        /*
        * 设要排序的数组是A[0]……A[N-1]，首先任意选取一个数据（通常选用数组的第一个数）作为关键数据，
        * 然后将所有比它小的数都放到它前面，所有比它大的数都放到它后面，这个过程称为一趟快速排序。
        * 值得注意的是，快速排序不是一种稳定的排序算法，也就是说，多个相同的值的相对位置也许会在算法结束时产生变动。
        一趟快速排序的算法是：
           1）设置两个变量i、j，排序开始的时候：i=0，j=N-1；
           2）以第一个数组元素作为关键数据，赋值给key，即key=A[0]；
           3）从j开始向前搜索，即由后开始向前搜索(j–)，找到第一个小于key的值A[j]，将A[j]和A[i]互换；
           4）从i开始向后搜索，即由前开始向后搜索(i++)，找到第一个大于key的A[i]，将A[i]和A[j]互换；
           5）重复第3、4步，直到i=j； (3,4步中，没找到符合条件的值，即3中A[j]不小于key,4中A[i]不大于key的时候改变j、i的值，
        *     使得j=j-1，i=i+1，直至找到为止。找到符合条件的值，进行交换的时候i， j指针位置不变。另外，i==j这一过程一定正好是i+或j-完成的时候，此时令循环结束）。
        */
        public static void QuickSort(this IList<int> data, int left, int right,int num)
        {
            if (left < right)
            {
                int middle = data[(left + right) / 2];
                int i = left - 1;
                int j = right + 1;
                while (true)
                {
                    while (data[++i] < middle && i < right) ;
                    while (data[--j] > middle && j > 0) ;
                    if (i >= j)
                    {
                        break;
                    }
                    int number = data[i];
                    data[i] = data[j];
                    data[j] = number;

                    num += 1;
                }
                QuickSort(data, left, i - 1, num);
                QuickSort(data, j + 1, right, num);
            }
        }
        #endregion

        #region 插入排序
        /*
        * 每次从无序表中取出第一个元素，把它插入到有序表的合适位置，使有序表仍然有序。
        * 第一趟比较前两个数，然后把第二个数按大小插入到有序表中； 第二趟把第三个数据与前两个数从前向后扫描，把第三个数按大小插入到有序表中；
        * 依次进行下去，进行了(n-1)趟扫描以后就完成了整个排序过程。
        * 直接插入排序属于稳定的排序，最坏时间复杂性为O(n^2)，空间复杂度为O(1)。
        * 直接插入排序是由两层嵌套循环组成的。外层循环标识并决定待比较的数值。内层循环为待比较数值确定其最终位置。
        * 直接插入排序是将待比较的数值与它的前一个数值进行比较，所以外层循环是从第二个数值开始的。
        * 当前一数值比待比较数值大的情况下继续循环比较，直到找到比待比较数值小的并将待比较数值置入其后一位置，结束该次循环。
        * 值得注意的是，我们必需用一个存储空间来保存当前待比较的数值，因为当一趟比较完成时，
        * 我们要将待比较数值置入比它小的数值的后一位 插入排序类似玩牌时整理手中纸牌的过程。
        * 插入排序的基本方法是：每步将一个待排序的记录按其关键字的大小插到前面已经排序的序列中的适当位置，直到全部记录插入完毕为止。
        */
        public static void InsertSort(this IList<int> data)
        {
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i] < data[i - 1])
                {
                    int temp = data[i];
                    int j = 0;
                    for (j = i - 1; j >= 0 && temp < data[j]; j--)
                    {
                        data[j + 1] = data[j];
                    }
                    data[j + 1] = temp;
                }
            }
        }
        #endregion

        #region 希尔排序
        /*
        * 希尔排序(Shell Sort)是插入排序的一种。也称缩小增量排序，是直接插入排序算法的一种更高效的改进版本。
        * 希尔排序是非稳定排序算法。该方法因DL．Shell于1959年提出而得名。

        * 希尔排序是基于插入排序的以下两点性质而提出改进方法的：
        * 插入排序在对几乎已经排好序的数据操作时，效率高，即可以达到线性排序的效率。
        * 但插入排序一般来说是低效的，因为插入排序每次只能将数据移动一位。

       基本思想：

        * 先取一个小于n的整数d1作为第一个增量，把文件的全部记录分组。所有距离为d1的倍数的记录放在同一个组中。
        * 先在各组内进行直接插入排序；然后，取第二个增量d2<d1重复上述的分组和排序，直至所取的增量 =1( < …<d2<d1)，
        * 即所有记录放在同一组中进行直接插入排序为止。
        * 该方法实质上是一种分组插入方法
        * 比较相隔较远距离（称为增量）的数，使得数移动时能跨过多个元素，
        * 则进行一次比[2] 较就可能消除多个元素交换。D.L.shell于1959年在以他名字命名的排序算法中实现了这一思想。
        * 算法先将要排序的一组数按某个增量d分成若干组，每组中记录的下标相差d.对每组中全部元素进行排序，
        * 然后再用一个较小的增量对它进行，在每组中再进行排序。当增量减到1时，整个要排序的数被分成一组，排序完成。
        * 一般的初次取序列的一半为增量，以后每次减半，直到增量为1。
        */
        public static void ShellSort(this IList<int> data)
        {
            int length = data.Count;
            for (int h = length / 2; h > 0; h = h / 2)
            {
                // 这里是插入排序
                for (int i = h; i < length; i++)
                {
                    int temp = data[i];
                    if (temp < data[i - h])
                    {
                        for (int j = 0; j < i; j += h)
                        {
                            if (temp < data[j])
                            {
                                temp = data[j];
                                data[j] = data[i];
                                data[i] = temp;
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
