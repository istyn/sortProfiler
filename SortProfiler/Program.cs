/////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Name                    :Isaac Styles
// Department Name : Computer and Information Sciences 
// File Name              :Program.cs
// Purpose                 :Define a class to fill a MasterList with a determined dataset,
//						copy that dataset to an array of Lists, and sort each using a different algorithm.
//							
// Author			: Isaac Styles, styles@goldmail.etsu.edu
// Create Date	: March 20, 2013
//
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
//
// Modified Date	: March 20, 2013
// Modified By		: Isaac Styles
//
/////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SortProfiler
{
	/// <summary>
	/// Fills a MasterList with a variably random set of integers, then copies the List into an array of Lists
	/// where each is sorted using a different sorting algorithm method.
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{

			const int NumberOfLists = 6;                    //number of Lists to be sorted

			const int N = 100;                               //number of integers in each List

			const int Randomness = 0;                       //0 thru 10 :: amount of randomness built into the MasterList
			//0 - Sorted Ascending
			//10 - Entirely Random

			Random Rand = new Random();                     //random instance used to fill list with integers

			List<int> MasterList = new List<int>(N);        //array of Lists to be sorted

			List<int>[] Lists = new List<int>[NumberOfLists];        //array of lists initialized to Const NUMBER OF LISTS

			//fill the masterList with random integers

			for (int x = 0; x < N; x++)
			{
				MasterList.Add(Rand.Next());
			}

			/* the following is a selection sort, but with the added ability to add a determined amount of randomness
			  * to the sort. If the mod10 of the index is less than const Randomness, then
			  * the positions within the list are not sorted, but rather left random.*/

			VariableRandomSort(MasterList, N, Randomness);
			//DescendingSort(MasterList);

			//initialize individual Lists in the array

			for (int i = 0; i < NumberOfLists; i++)
			{
				Lists[i] = new List<int>(N);
			}

			//add individual elements of MasterList into easy List

			for (int listIndex = 0; listIndex < Lists.Length; listIndex++)
			{
				for (int x = 0; x < N; x++)
				{
					Lists[listIndex].Add(MasterList[x]);
				}
			}

			//sort the Lists using various sorting algorithm methods

			SinkSort(Lists[0]);
			SelectionSort(Lists[1], N);
			InsertionSort(Lists[2]);
			QuickSort(Lists[3]);
			MedianOfThreeQuickSort(Lists[4]);
			ShellSort(Lists[5]);



			/*
			for (int x = 0; x < N; x++)
			{
				Console.Write("\n\nObj " + x + ":  " + MasterList[x] + "  " + Lists[1][x] + "  " + Lists[2][x] + "  " + Lists[3][x] + "  " + Lists[4][x] + "  " + Lists[5][x]);
				if (!(Lists[0][x] == Lists[1][x]&&Lists[1][x]==Lists[2][x]&&Lists[1][x]==Lists[2][x]&&Lists[2][x]==Lists[3][x]&&Lists[3][x]==Lists[4][x]&&Lists[4][x]==Lists[5][x]))
					Console.Write("  ERROR!!!!");
			}
		   
			Console.ReadKey();*/
		}

		#region VariableRandomSort
		/// <summary>
		/// A selection sort with the added capability of adding an amount of randomness to the sort.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="n">The number of integers within the list.</param>
		/// <param name="randomness">The randomness: 0 thru 10.</param>
		/// <param name="pass">The pass counter. FOR RECURSION ONLY</param>
		private static void VariableRandomSort(List<int> list, int n, int randomness, int pass = 0)
		{

			if (n <= 1)
				return;

			int max = Max(list, n);
			if (list[max] != list[n - 1] && pass % 10 >= randomness)
				Swap(list, max, n - 1);

			VariableRandomSort(list, n - 1, randomness, pass + 1);
		}
		#endregion


		#region DescendingSort
		/// <summary>
		/// Used to sort the MasterList dataset into descending order.
		/// </summary>
		/// <param name="MasterList">The master list.</param>
		private static void DescendingSort(List<int> MasterList)
		{
			int N = MasterList.Count;
			ShellSort(MasterList);
			for (int x = 0; x < N / 2; x++)
			{
				Swap(MasterList, x, N - 1 - x);
			}
		}
		#endregion


		#region SinkSort

		/// <summary>
		/// Sorts a list using a sink sort
		/// </summary>
		/// <param name="list">The list.</param>
		private static void SinkSort(List<int> list)
		{
			int n = list.Count;

			bool sorted = false;
			int pass = 0;
			while (!sorted && (pass < n))
			{
				pass++;
				sorted = true;

				for (int i = 0; i < n - pass; i++)
				{
					if (list[i] > list[i + 1])
					{
						Swap(list, i, i + 1);
						sorted = false;
					}
				}
			}
		}
		#endregion


		#region SelectionSort
		/// <summary>
		/// Sorts the list of size n using selection sort algorithm
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="n">The size of list.</param>
		private static void SelectionSort(List<int> list, int n)
		{
			if (n <= 1)
				return;

			int max = Max(list, n);
			if (list[max] != list[n - 1])
				Swap(list, max, n - 1);

			SelectionSort(list, n - 1);
		}
		#endregion


		#region InsertionSort
		/// <summary>
		/// Sorts a list using an insertion sorting algorithm.
		/// </summary>
		/// <param name="list">The list.</param>
		private static void InsertionSort(List<int> list)
		{
			int n = list.Count;

			for (int x = 1; x < n; x++)
			{
				int j = x;
				while (j > 0)
				{
					if (list[j - 1] > list[j])
					{
						int temp = list[j - 1];
						list[j - 1] = list[j];
						list[j] = temp;
						j--;
					}
					else
						break;
				}
			}
		}
		#endregion


		#region MedianOfThreeQuickSort

		/// <summary>
		/// Sorts a list using a median-of-three quick sort, recursive algorithm.
		/// </summary>
		/// <param name="list">The list.</param>
		private static void MedianOfThreeQuickSort(List<int> list)
		{
			MedianOfThreeQuickSort(list, 0, list.Count - 1);
		}

		private static void MedianOfThreeQuickSort(List<int> list, int start, int end)
		{
			const int cutoff = 10;          //point to switch to InsertionSort

			if (start + cutoff > end)
				InsertionSort(list);

			else
			{
				int middle = (start + end) / 2;
				if (list[middle] < list[start])
					Swap(list, start, middle);
				if (list[end] < list[start])
					Swap(list, start, end);
				if (list[end] < list[middle])
					Swap(list, middle, end);

				//above if statements guarantee list[end] >= list[middle], so set pivot
				int pivot = list[middle];
				Swap(list, middle, end - 1);

				//partitioning
				int left, right;
				for (left = start, right = end - 1; ; )
				{
					while (list[++left] < pivot)
						;
					while (pivot < list[--right])
						;
					if (left < right)
						Swap(list, left, right);
					else
						break;

				}

				//Restore pivot
				Swap(list, left, end - 1);

				MedianOfThreeQuickSort(list, start, left - 1);
				MedianOfThreeQuickSort(list, left + 1, end);
			}
		}
		#endregion


		#region OriginalQuickSort

		/// <summary>
		/// Sorts a list using the original quick sort, recursive algorithm
		/// </summary>
		/// <param name="list">The list.</param>
		private static void QuickSort(List<int> list)
		{
			QuickSort(list, 0, list.Count - 1);
		}

		private static void QuickSort(List<int> list, int start, int end)
		{
			int left = start;
			int right = end;

			if (left >= right)
				return;

			//partition into left and right subsets
			while (left < right)
			{
				while (list[left] <= list[right] && left < right)
					right--;           //burn candle from right

				if (left < right)
					Swap(list, left, right);

				while (list[left] <= list[right] && left < right)
					left++;           //burn candle from left

				if (left < right)
					Swap(list, left, right);
			}
			QuickSort(list, start, left - 1);       //recursively sort left partition
			QuickSort(list, right + 1, end);        //recursively sort right partition
		}
		#endregion


		#region ShellSort
		/// <summary>
		/// Sorts a list using shell sort algorithm, with gap = 2.2
		/// </summary>
		/// <param name="list">The list.</param>
		private static void ShellSort(List<int> list)
		{
			for (int gap = list.Count / 2; gap > 0; gap = (gap == 2 ? 1 : (int)(gap / 2.2)))
			{
				int temp, j;
				for (int i = gap; i < list.Count; i++)
				{
					temp = list[i];

					for (j = i; j >= gap && temp < list[j - gap]; j -= gap)
					{
						list[j] = list[j - gap];
					}

					list[j] = temp;
				}
			}
		}
		#endregion


		#region Swap and Max Methods
		/// <summary>
		/// Swaps the specified integers within the list
		/// </summary>
		/// <param name="List">The list.</param>
		/// <param name="n">The n.</param>
		/// <param name="m">The m.</param>
		private static void Swap(List<int> List, int n, int m)
		{
			int temp = List[n];
			List[n] = List[m];
			List[m] = temp;
		}

		/// <summary>
		/// Determines the max value in the list of size n
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="n">The size of the list.</param>
		/// <returns></returns>
		private static int Max(List<int> list, int n)
		{
			int max = 0;

			for (int i = 0; i < n; i++)
			{
				if (list[max] < list[i])
					max = i;
			}

			return max;
		}
		#endregion

	}
}
