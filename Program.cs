using System;

namespace Lab4
{
	// Новий абстрактний базовий клас — керує Random і описує інтерфейс
	abstract class MatrixBase
	{
		protected readonly Random _rng;

		// Конструктор приймає Random (для детермінованого тестування можна передати new Random(seed))
		protected MatrixBase(Random? rng = null) => _rng = rng ?? Random.Shared;
		protected MatrixBase(int seed) => _rng = new Random(seed);

		public abstract void ReadFromKeyboard();
		public abstract void FillRandom(int min = -10, int max = 10);
		public abstract int GetMin();
		public abstract void Print();
	}

	class Matrix2D : MatrixBase
	{
		// Ініціалізація масиву переміщена в конструктор
		private int[,] _a;
		public int Rows { get; }
		public int Cols { get; }

		// Конструктори: розмір + Random або seed
		public Matrix2D(int rows = 3, int cols = 3, Random? rng = null) : base(rng)
		{
			if (rows <= 0 || cols <= 0) throw new ArgumentException("Rows and Cols must be positive.");
			Rows = rows;
			Cols = cols;
			_a = new int[Rows, Cols];
		}

		public Matrix2D(int rows, int cols, int seed) : base(seed)
		{
			if (rows <= 0 || cols <= 0) throw new ArgumentException("Rows and Cols must be positive.");
			Rows = rows;
			Cols = cols;
			_a = new int[Rows, Cols];
		}

		// Інкапсуляція доступу
		public int GetElement(int i, int j)
		{
			if (i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException(nameof(i), "Індекси виходять за межі матриці.");
			return _a[i, j];
		}

		public void SetElement(int i, int j, int value)
		{
			if (i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException(nameof(i), "Індекси виходять за межі матриці.");
			_a[i, j] = value;
		}

		public override void ReadFromKeyboard()
		{
			Console.WriteLine($"Введіть {Rows * Cols} цілих чисел для двовимірної матриці {Rows}x{Cols}:");
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Cols; j++)
				{
					while (true)
					{
						Console.Write($"A[{i}][{j}] = ");
						string? s = Console.ReadLine();
						if (int.TryParse(s, out int v))
						{
							SetElement(i, j, v);
							break;
						}
						Console.WriteLine("Невірне число, спробуйте ще.");
					}
				}
		}

		public override void FillRandom(int min = -10, int max = 10)
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Cols; j++)
					SetElement(i, j, _rng.Next(min, max + 1));
		}

		public override int GetMin()
		{
			int min = GetElement(0, 0);
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Cols; j++)
				{
					int val = GetElement(i, j);
					if (val < min) min = val;
				}
			return min;
		}

		public override void Print()
		{
			Console.WriteLine($"2D Matrix ({Rows}x{Cols}):");
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Cols; j++)
					Console.Write($"{GetElement(i, j),6}");
				Console.WriteLine();
			}
		}
	}

	class Matrix3D : MatrixBase
	{
		private int[,,] _b;
		public int Depth { get; }
		public int Rows { get; }
		public int Cols { get; }

		// Конструктори: розміри + Random або seed
		public Matrix3D(int depth = 3, int rows = 3, int cols = 3, Random? rng = null) : base(rng)
		{
			if (depth <= 0 || rows <= 0 || cols <= 0) throw new ArgumentException("Dimensions must be positive.");
			Depth = depth;
			Rows = rows;
			Cols = cols;
			_b = new int[Depth, Rows, Cols];
		}

		public Matrix3D(int depth, int rows, int cols, int seed) : base(seed)
		{
			if (depth <= 0 || rows <= 0 || cols <= 0) throw new ArgumentException("Dimensions must be positive.");
			Depth = depth;
			Rows = rows;
			Cols = cols;
			_b = new int[Depth, Rows, Cols];
		}

		public int GetElement3D(int k, int i, int j)
		{
			if (k < 0 || k >= Depth || i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3D.");
			return _b[k, i, j];
		}

		public void SetElement3D(int k, int i, int j, int value)
		{
			if (k < 0 || k >= Depth || i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3D.");
			_b[k, i, j] = value;
		}

		public override void ReadFromKeyboard()
		{
			Console.WriteLine($"Введіть {Depth * Rows * Cols} цілих чисел для тривимірної матриці {Depth}x{Rows}x{Cols}:");
			for (int k = 0; k < Depth; k++)
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Cols; j++)
					{
						while (true)
						{
							Console.Write($"B[{k}][{i}][{j}] = ");
							string? s = Console.ReadLine();
							if (int.TryParse(s, out int v))
							{
								SetElement3D(k, i, j, v);
								break;
							}
							Console.WriteLine("Невірне число, спробуйте ще.");
						}
					}
		}

		public override void FillRandom(int min = -10, int max = 10)
		{
			for (int k = 0; k < Depth; k++)
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Cols; j++)
						SetElement3D(k, i, j, _rng.Next(min, max + 1));
		}

		public override int GetMin()
		{
			int min = GetElement3D(0, 0, 0);
			for (int k = 0; k < Depth; k++)
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Cols; j++)
					{
						int val = GetElement3D(k, i, j);
						if (val < min) min = val;
					}
			return min;
		}

		public override void Print()
		{
			Console.WriteLine($"3D Matrix ({Depth}x{Rows}x{Cols}):");
			for (int k = 0; k < Depth; k++)
			{
				Console.WriteLine($"Layer {k}:");
				for (int i = 0; i < Rows; i++)
				{
					for (int j = 0; j < Cols; j++)
						Console.Write($"{GetElement3D(k, i, j),6}");
					Console.WriteLine();
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			// Демонстрація: випадкове заповнення з Random.Shared
			var m2 = new Matrix2D(); // за замовчуванням 3x3, використовує Random.Shared
			m2.FillRandom(-20, 20);
			m2.Print();
			Console.WriteLine($"Мінімальний елемент 2D матриці: {m2.GetMin()}");
			Console.WriteLine();

			// Детермінований приклад: передаємо seed для відтворюваності
			var m3 = new Matrix3D(3, 3, 3, seed: 42); // seed дає детермінований Random
			m3.FillRandom(-20, 20);
			m3.Print();
			Console.WriteLine($"Мінімальний елемент 3D матриці: {m3.GetMin()}");

			Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
			Console.ReadKey();
		}
	}
}