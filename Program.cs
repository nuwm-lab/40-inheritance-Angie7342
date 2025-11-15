using System;

namespace Lab4
{
	class Matrix2D
	{
		// Замість protected поля робимо приватне поле
		private int[,] _a = new int[3, 3];

		// змінено доступність RNG, щоб похідний клас міг його використовувати без рефлексії
		protected static readonly Random _rng = new Random();

		// Розміри як властивості (тільки для читання)
		public int Rows => 3;
		public int Cols => 3;

		// Метод доступу з перевіркою меж (инкапсуляція)
		public int GetElement(int i, int j)
		{
			if (i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3x3.");
			return _a[i, j];
		}

		public void SetElement(int i, int j, int value)
		{
			if (i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3x3.");
			_a[i, j] = value;
		}

		// Заповнення з клавіатури
		public virtual void ReadFromKeyboard()
		{
			Console.WriteLine("Введіть 9 цілих чисел для двовимірної матриці 3x3:");
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

		// Заповнення випадковими числами
		public virtual void FillRandom(int min = -10, int max = 10)
		{
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Cols; j++)
					SetElement(i, j, _rng.Next(min, max + 1));
		}

		// Знаходження мінімального елемента
		public virtual int GetMin()
		{
			int min = GetElement(0, 0);
			for (int i = 0; i < Rows; i++)
				for (int j = 0; j < Cols; j++)
					if (GetElement(i, j) < min) min = GetElement(i, j);
			return min;
		}

		public virtual void Print()
		{
			Console.WriteLine("2D Matrix (3x3):");
			for (int i = 0; i < Rows; i++)
			{
				for (int j = 0; j < Cols; j++)
					Console.Write($"{GetElement(i, j),4}");
				Console.WriteLine();
			}
		}

		// public wrapper methods (зручні іменовані методи)
		public void SetElementsFromKeyboard() => ReadFromKeyboard();
		public void SetElementsRandom(int min = -10, int max = 10) => FillRandom(min, max);
		public int FindMinElement() => GetMin();
	}

	class Matrix3D : Matrix2D
	{
		// Власне приватне поле для 3D, не відкриваємо його назовні
		private int[,,] _b = new int[3, 3, 3];

		public int Depth => 3;

		// Методи доступу для 3D з перевіркою індексів
		public int GetElement3D(int k, int i, int j)
		{
			if (k < 0 || k >= Depth || i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3x3x3.");
			return _b[k, i, j];
		}

		public void SetElement3D(int k, int i, int j, int value)
		{
			if (k < 0 || k >= Depth || i < 0 || i >= Rows || j < 0 || j >= Cols)
				throw new ArgumentOutOfRangeException("Індекси виходять за межі матриці 3x3x3.");
			_b[k, i, j] = value;
		}

		// Перевизначаємо зчитування для 3D, використовуємо SetElement3D
		public override void ReadFromKeyboard()
		{
			Console.WriteLine("Введіть 27 цілих чисел для тривимірної матриці 3x3x3:");
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

		// Перевизначаємо заповнення випадковими числами для 3D — використовуємо захищений _rng
		public override void FillRandom(int min = -10, int max = 10)
		{
			for (int k = 0; k < Depth; k++)
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Cols; j++)
						SetElement3D(k, i, j, _rng.Next(min, max + 1));
		}

		// Додаємо ті самі зручні wrapper-методи для 3D
		public void SetElementsFromKeyboard3D() => ReadFromKeyboard();
		public void SetElementsRandom3D(int min = -10, int max = 10) => FillRandom(min, max);
		public int FindMinElement3D() => GetMin();

		// Перевизначаємо пошук мінімуму для 3D
		public override int GetMin()
		{
			int min = GetElement3D(0, 0, 0);
			for (int k = 0; k < Depth; k++)
				for (int i = 0; i < Rows; i++)
					for (int j = 0; j < Cols; j++)
						if (GetElement3D(k, i, j) < min) min = GetElement3D(k, i, j);
			return min;
		}

		public override void Print()
		{
			Console.WriteLine("3D Matrix (3x3x3):");
			for (int k = 0; k < Depth; k++)
			{
				Console.WriteLine($"Layer {k}:");
				for (int i = 0; i < Rows; i++)
				{
					for (int j = 0; j < Cols; j++)
						Console.Write($"{GetElement3D(k, i, j),4}");
					Console.WriteLine();
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			// Демонстрація роботи
			var m2 = new Matrix2D();
			m2.FillRandom(-20, 20);
			m2.Print();
			Console.WriteLine($"Мінімальний елемент 2D матриці: {m2.GetMin()}");
			Console.WriteLine();

			var m3 = new Matrix3D();
			// Використовуємо ReadFromKeyboard або FillRandom; FillRandom у 3D створює локальний Random
			m3.FillRandom(-20, 20);
			m3.Print();
			Console.WriteLine($"Мінімальний елемент 3D матриці: {m3.GetMin()}");

			Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
			Console.ReadKey();
		}
	}
}