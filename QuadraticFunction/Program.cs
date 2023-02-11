
namespace QuadraticFunction
{
    class Program
    {
        #region Zmienne statyczne

        private static ConsoleKey choice;
        private static string hint = "", principle = "", userName = GetUserName();
        private static float a, b, c, p, q, x1, x2;
        private static double delta, elementOfTheDelta, firstTheZero, secondTheZero, pointP, pointQ;

        #endregion

        #region Funkcja główna rozpoczynająca działanie programu

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            GetAppInfo();
            GreetUser(userName);

            do
            {
                Console.Clear();
                MenuConfiguration();
                ExitInformation();
            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        #endregion

        #region Informacje i powitanie

        static void GetAppInfo()
        {
            string appName = "Funkcja kwadratowa";
            Console.Title = appName;
            int major = 1; int minor = 0; int release = 0;
            string appVersion = $"{major}.{minor}.{release}";
            string appAuthor = "Marcin Dobrzyński";

            string information = $"[{appName}] Wersja: {appVersion}, Autor: {appAuthor}.";

            PrintColorWriteLine(ConsoleColor.Magenta, information);
        }

        static string GetUserName()
        {
            string inputUserName;

            do
            {
                Console.Write("Witaj, jak masz na imię? ");
                inputUserName = Console.ReadLine();
                Console.Clear();
                GetAppInfo();
            } while (inputUserName == "" || inputUserName == null);

            return inputUserName;
        }

        static void GreetUser(string userName)
        {
            string greet = $"Dziękuję {char.ToUpper(userName[0]) + userName.Substring(1)} za skorzystanie z mojej aplikacji.";
            PrintColorWriteLine(ConsoleColor.Blue, greet);
            string informationForUser = "Mam nadzieję, że program, który stworzyłem, Ci się przyda :)";
            PrintColorWriteLine(ConsoleColor.Yellow, informationForUser);
            Loading(5, 1500, false, false);
        }

        #endregion

        #region Menu programu

        static void MenuConfiguration()
        {
            string help = "W czym mogę Ci pomóc ?";
            PrintColorWriteLine(ConsoleColor.Gray, help);

            string menuSelection =
                "1 - Postać ogólna ( f(x) = ax² + bx + c )\n" +
                "2 - Postać kanoniczna ( f(x) = a(x - p)² + q )\n" +
                "3 - Postać iloczynowa ( f(x) = a(x - x₁)(x - x₂) )\n" +
                "4 - Zamknij program";
            PrintColorWriteLine(ConsoleColor.Gray, menuSelection);
            Thread.Sleep(750);

            hint = "!Podpowiedź: zapisz liczbę odpowiednią do akcji!";
            PrintColorWriteLine(ConsoleColor.Red, hint);
            string userChoice = "Mój wybór: ";
            PrintColorWrite(ConsoleColor.Gray, userChoice);

            choice = Console.ReadKey().Key;
            Console.WriteLine();

            switch(choice)
            {
                case ConsoleKey.D1: GeneralForm(); break;
                case ConsoleKey.D2: CanonicalForm(); break;
                case ConsoleKey.D3: ProductForm(); break;
                case ConsoleKey.D4: Environment.Exit(1); break;
                default:
                    hint = "\nNiepoprawny numer, spróbuj jeszcze raz.";
                    PrintColorWriteLine(ConsoleColor.Red, hint);
                break;
            }
        }

        #endregion

        #region Funkcja kwadratowa

        static void GeneralForm()
        {
            PrintColorWrite(ConsoleColor.Yellow, "\nPodaj a: ");
            a = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj b: ");
            b = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj c: ");
            c = float.Parse(Console.ReadLine());

            if(a == 0)
            {
                string linearFunction = $"{char.ToUpper(userName[0]) + userName.Substring(1)}, jeżeli współczynnik a jest równy 0, to Twoja funkcja staje się liniową o wzorze: f(x) = {b}x + {c}.";
                string linearFunctionPattern = $"f(x) = ax + b, gdzie a (współczynnik kierunkowy prostej) = {b}, b (wyraz wolny) = {c}.";
                PrintColorWriteLine(ConsoleColor.Red, linearFunction);
                PrintColorWriteLine(ConsoleColor.Yellow, linearFunctionPattern);
                Thread.Sleep(1000);

                hint = "Spróbuj jeszcze raz.\n";
                PrintColorWrite(ConsoleColor.Gray, hint);
                Thread.Sleep(1000);

                ExitInformation();

                choice = Console.ReadKey().Key;

                if(choice == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MenuConfiguration();
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            else
            {
                string userGeneralForm = "\nPostać ogólna:\n" + 
                                         $"f(x) = ({a})x² + ({b})x + ({c})" +
                                         $"\na = {a}, b = {b}, c = {c}";
                PrintColorWriteLine(ConsoleColor.Yellow, userGeneralForm);
                Thread.Sleep(1000);

                principle = "Gdzie literki a, b oraz c są współczynnikami liczbowymi i a ≠ 0.\n" +
                            "Wykresem każdej funkcji kwadratowej jest parabola.";
                PrintColorWriteLine(ConsoleColor.Gray, principle);

                if(a > 0)
                {
                    principle = $"Ramiona paraboli są skierowane do góry, ponieważ współczynnik a = {a} jest dodatni.";
                    PrintColorWriteLine(ConsoleColor.Gray, principle);
                }
                else if(a < 0)
                {
                    principle = $"Ramiona paraboli są skierowane w dół, ponieważ współczynnik a = {a} jest ujemny.";
                    PrintColorWriteLine(ConsoleColor.Gray, principle);
                }

                principle = $"Punkt przecięcia z osią 0Y jest równy c, czyli {c}.";
                PrintColorWriteLine(ConsoleColor.Gray, principle);
                Thread.Sleep(1000);

                principle = "\nJeżeli Δ > 0 to funkcja posiada dwa miejsca zerowe.\n" +
                            "Jeżeli Δ < 0 to funkcja posiada jedno miejsce zerowe.\n" +
                            "Jeżeli Δ = 0 to funkcja nie posiada miejsc zerowych.";
                PrintColorWriteLine(ConsoleColor.Gray, principle);
                Thread.Sleep(1000);

                Delta(a, b, c);
                ZeroPlaces(a, b, delta);
                Vertex(a, b, delta);
                VertexCoordinates(pointP, pointQ);
                AxisOfSymmetry(pointP);
                SetOfValues(a, pointQ);
                Monotonicity(a, pointP);

                string canonicalForm = "\nPostać kanoniczna:\n" + 
                                       "f(x) = a(x - p)² + q";
                string userCanonicalForm = $"f(x) = {a}(x - ({pointP}))² + ({pointQ})";
                PrintColorWriteLine(ConsoleColor.Yellow, canonicalForm);
                PrintColorWriteLine(ConsoleColor.Cyan, userCanonicalForm);
                Thread.Sleep(1000);

                if(delta > 0)
                {
                    string productForm = "\nPostać iloczynowa:\n" + 
                                         "f(x) = a(x - x₁)(x - x₂)";
                    string userProductForm = $"f(x) = {a}(x - ({-firstTheZero}))(x - ({-secondTheZero}))";
                    PrintColorWriteLine(ConsoleColor.Yellow, productForm);
                    PrintColorWriteLine(ConsoleColor.Cyan, userProductForm);
                    Thread.Sleep(1000);
                }
            }
        }

        static void CanonicalForm()
        {
            PrintColorWrite(ConsoleColor.Yellow, "\nPodaj a: ");
            a = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj p: ");
            p = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj q: ");
            q = float.Parse(Console.ReadLine());

            if(a == 0)
            {
                principle = "Współczynnik a musi być różny od 0.\n";
                hint = "Spróbuj jeszcze raz.\n";
                PrintColorWrite(ConsoleColor.Red, principle);
                PrintColorWrite(ConsoleColor.Gray, hint);
                Thread.Sleep(1000);

                ExitInformation();

                choice = Console.ReadKey().Key;

                if(choice == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MenuConfiguration();
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            else if(a < 0 || a > 0) 
            {
                string userCanonicalForm = "\nPostać kanoniczna:\n" + 
                                           $"f(x) = {a}(x - ({p}))² + ({q})" +
                                           $"\na = {a}, p = {p}, q = {q}";
                PrintColorWriteLine(ConsoleColor.Yellow, userCanonicalForm);
                Thread.Sleep(1000);

                principle = "Gdzie literki a, p oraz q są współczynnikami liczbowymi i a ≠ 0.\n" +
                            "Wykresem każdej funkcji kwadratowej jest parabola.";
                PrintColorWriteLine(ConsoleColor.Gray, principle);

                VertexCoordinates(p, q);
                AxisOfSymmetry(p);
                SetOfValues(a, q);
                Monotonicity(a, p);
            }
        }

        static void ProductForm()
        {
            PrintColorWrite(ConsoleColor.Yellow, "\nPodaj a: ");
            a = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj x₁: ");
            x1 = float.Parse(Console.ReadLine());
            PrintColorWrite(ConsoleColor.Yellow, "Podaj x₂: ");
            x2 = float.Parse(Console.ReadLine());

            if(a == 0)
            {
                principle = "Współczynnik a musi być różny od 0.\n";
                hint = "Spróbuj jeszcze raz.\n";
                PrintColorWrite(ConsoleColor.Red, principle);
                PrintColorWrite(ConsoleColor.Gray, hint);
                Thread.Sleep(1000);

                ExitInformation();

                choice = Console.ReadKey().Key;

                if (choice == ConsoleKey.Enter)
                {
                    Console.Clear();
                    MenuConfiguration();
                }
                else
                {
                    Environment.Exit(1);
                }
            }
            else if(a < 0 || a > 0)
            {
                string userCanonicalForm = "\nPostać iloczynowa:\n" +
                                       $"f(x) = {a}(x - ({-x1}))(x - ({-x2}))\n" +
                                       $"a = {a}, x₁ = {x1}, x₂ = {x2}";
                PrintColorWriteLine(ConsoleColor.Yellow, userCanonicalForm);
                Thread.Sleep(1000);

                principle = "Gdzie a jest współczynnikiem liczbowym, takim, że a ≠ 0. Literki x₁ i x₂ są miejscami zerowymi funkcji f(x).\n" +
                            "Delta (Δ) jest zawsze większa od 0.\n" +
                            "Wykresem każdej funkcji kwadratowej jest parabola.";
                PrintColorWriteLine(ConsoleColor.Gray, principle);
                hint = "Jeżeli funkcja kwadratowa nie ma miejsc zerowych, to postać iloczynowa nie istnieje!";
                PrintColorWriteLine(ConsoleColor.Red, hint);
            }
        }

        static void Delta(float a, float b, float c)
        {
            string deltaPattern = "\nDelta (Δ):\n" +
                                  "Δ = b² - 4 * a * c";
            string userDeltaPattern = $"Δ = ({b})² - 4 * {a} * {c}";
            delta = (Math.Pow(b, 2)) - 4 * a * c;
            string userDelta = $"Δ = {delta}";

            PrintColorWriteLine(ConsoleColor.Yellow, deltaPattern);
            PrintColorWriteLine(ConsoleColor.Yellow, userDeltaPattern);
            PrintColorWriteLine(ConsoleColor.Cyan, userDelta);
        }

        static void ZeroPlaces(float a, float b, double delta)
        {
            elementOfTheDelta = Math.Round(Math.Sqrt(delta), 2);
            string userElementOfTheDelta = $"√Δ = {Math.Sqrt(delta)}";

            string firstTheZeroPattern;
            string userFirstTheZeroPattern;
            firstTheZero = (-b - Math.Sqrt(delta)) / (2 * a);
            string userFirstTheZero;

            if(delta > 0)
            {
                PrintColorWriteLine(ConsoleColor.Cyan, userElementOfTheDelta);
                Thread.Sleep(1000);

                firstTheZeroPattern = "\nPierwsze miejsce zerowe:\n" +
                                      "x₁ = -b - √Δ / 2 * a";
                userFirstTheZeroPattern = $"x₁ = {-b} - √{delta} / 2 * {a}";
                userFirstTheZero = $"x₁ = {firstTheZero}";

                string secondTheZeroPattern = "\nDrugie miejsce zerowe:\n" +
                                              "x₂ = -b + √Δ / 2 * a";
                string userSecondTheZeroPattern = $"x₂ = {-b} + √{delta} / 2 * {a}";
                secondTheZero = (-b + Math.Sqrt(delta)) / (2 * a);
                string userSecondTheZero = $"x₂ = {secondTheZero}";

                PrintColorWriteLine(ConsoleColor.Yellow, firstTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Yellow, userFirstTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Cyan, userFirstTheZero);
                Thread.Sleep(1000);

                PrintColorWriteLine(ConsoleColor.Yellow, secondTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Yellow, userSecondTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Cyan, userSecondTheZero);
                Thread.Sleep(1000);

                VietaPatterns(a, b, c);
            }
            else if(delta < 0)
            {
                string informationTheZeros = "Twoja funkcja kwadratowa nie posiada miejsc zerowych.";
                PrintColorWriteLine(ConsoleColor.Red, informationTheZeros);
                Thread.Sleep(1000);
            }
            else if(delta == 0)
            {
                firstTheZeroPattern = "\nx₀ = -b - √Δ / 2 * a";
                userFirstTheZeroPattern = $"x₀ = {-b} - √{delta} / 2 * {a}";
                userFirstTheZero = $"x₀ = {firstTheZero}";

                PrintColorWriteLine(ConsoleColor.Cyan, userElementOfTheDelta);
                Thread.Sleep(1000);

                PrintColorWriteLine(ConsoleColor.Yellow, firstTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Yellow, userFirstTheZeroPattern);
                PrintColorWriteLine(ConsoleColor.Cyan, userFirstTheZero);
                Thread.Sleep(1000);
            }
        }

        static void Vertex(float a, float b, double delta)
        {
            string pointP_OfTheVertexPattern = "\nPunkt p (x) wierzchołka:\n" +
                                               "p = -b / 2 * a";
            string userPointP_OfTheVertexPattern = $"p = {-b} / 2 * {a}";
            pointP = (-b) / (2 * a);
            string userPointP = $"p = {pointP}";
            PrintColorWriteLine(ConsoleColor.Yellow, pointP_OfTheVertexPattern);
            PrintColorWriteLine(ConsoleColor.Yellow, userPointP_OfTheVertexPattern);
            PrintColorWriteLine(ConsoleColor.Cyan, userPointP);
            Thread.Sleep(1000);

            string pointQ_OfTheVertexPattern = "\nPunkt q (y) wierzchołka:\n" +
                                               "q = -Δ / 4 * a";
            string userPointQ_OfTheVertexPattern = $"q = {-delta} / 4 * {a}";
            pointQ = (-delta) / (4 * a);
            string userPointQ = $"q = {pointQ}";
            PrintColorWriteLine(ConsoleColor.Yellow, pointQ_OfTheVertexPattern);
            PrintColorWriteLine(ConsoleColor.Yellow, userPointQ_OfTheVertexPattern);
            PrintColorWriteLine(ConsoleColor.Cyan, userPointQ);
            Thread.Sleep(1000);
        }

        static void VertexCoordinates(double p, double q)
        {
            string vertexPatternPQ = "\nWspółrzędne wierzchołka:\n" +
                                     "W(p, q)";
            string vertexPatternXY = "\nNa układzie współrzędnych kartezjańskich:\n" +
                                     "W(x, y)";
            string userVertex = $"W({p}, {q})";
            PrintColorWriteLine(ConsoleColor.Yellow, vertexPatternPQ);
            PrintColorWriteLine(ConsoleColor.Cyan, userVertex);
            Thread.Sleep(1000);
            PrintColorWriteLine(ConsoleColor.Yellow, vertexPatternXY);
            PrintColorWriteLine(ConsoleColor.Cyan, userVertex);
            Thread.Sleep(1000);
        }

        static void AxisOfSymmetry(double p)
        {
            string informationAxisOfSymmetry = "\nOś symetrii funkcji kwadratowej:\n" +
                                               "x = p";
            string userAxisOfSymmetry = $"x = {p}";

            PrintColorWriteLine(ConsoleColor.Yellow, informationAxisOfSymmetry);
            PrintColorWriteLine(ConsoleColor.Cyan, userAxisOfSymmetry);
        }

        static void SetOfValues(float a, double q)
        {
            string informationSetofValues;
            string userSetOfValues;

            if(a > 0)
            {
                informationSetofValues = "\nJeżeli a jest większe od 0, czyli ramiona paraboli są skierowane w górę, to zbiorem wartości funkcji jest przedział <q,+∞).";
                userSetOfValues = $"Zbiorem wartości twojej funkcji f jest przedział: <{q},+∞).";
                PrintColorWriteLine(ConsoleColor.Gray, informationSetofValues);
                PrintColorWriteLine(ConsoleColor.Cyan, userSetOfValues);
            }
            else if(a < 0)
            {
                informationSetofValues = "\nJeżeli a jest mniejsze od 0, czyli ramiona paraboli są skierowane w dół, to zbiorem wartości funkcji jest przedział (-∞, q>.";
                userSetOfValues = $"Zbiorem wartości twojej funkcji f jest przedział: (-∞, {q}>.";
                PrintColorWriteLine(ConsoleColor.Gray, informationSetofValues);
                PrintColorWriteLine(ConsoleColor.Cyan, userSetOfValues);
            }
            Thread.Sleep(1000);
        }

        static void Monotonicity(float a, double p)
        {
            string informationMonotonicity;
            string userMonotonicity;

            if(a > 0)
            {
                informationMonotonicity = "\nJeżeli współczynnik a jest większy od 0, wówczas ramiona paraboli są skierowane do góry to funkcja f: maleje w przedziale (-∞, p>, a rośnie w przedziale <p, +∞).";
                userMonotonicity = $"Twoja funkcja f maleje w przedziale (-∞, {p}>, a rośnie w przedziale <{p}, +∞).";
                PrintColorWriteLine(ConsoleColor.Gray, informationMonotonicity);
                PrintColorWriteLine(ConsoleColor.Cyan, userMonotonicity);
            }
            else if(a < 0)
            {
                informationMonotonicity = "\nJeżeli współczynnik a jest mniejszy od 0, wówczas ramiona paraboli są skierowane w dół, to funkcja f: maleje w przedziale <p, +∞), a rośnie w przedziale (-∞, p>.";
                userMonotonicity = $"Twoja funkcja f maleje w przedziale <{p}, +∞), a rośnie w przedziale (-∞, {p}>.";
                PrintColorWriteLine(ConsoleColor.Gray, informationMonotonicity);
                PrintColorWriteLine(ConsoleColor.Cyan, userMonotonicity);
            }
            Thread.Sleep(1000);
        }

        static void VietaPatterns(float a, float b, float c)
        {
            string sumOfRootsPattern = "\nSuma pierwiastków:\n" +
                                       "x₁ + x₂ = -b / a";
            string userSumOfRootsPattern = $"x₁ + x₂ = {-b} / {a}";
            string userSumOfRoots = $"x₁ + x₂ = {-b / a}";

            string productOfRootsPattern = "\nIloczyn pierwiastków:\n" +
                                           "x₁ * x₂ = c / a";
            string userProductOfRootsPattern = $"x₁ * x₂ = {c} / {a}";
            string userProductOfRoots = $"x₁ * x₂ = {c / a}";

            PrintColorWriteLine(ConsoleColor.Yellow, sumOfRootsPattern);
            PrintColorWriteLine(ConsoleColor.Yellow, userSumOfRootsPattern);
            PrintColorWriteLine(ConsoleColor.Cyan, userSumOfRoots);
            Thread.Sleep(1000);

            PrintColorWriteLine(ConsoleColor.Yellow, productOfRootsPattern);
            PrintColorWriteLine(ConsoleColor.Yellow, userProductOfRootsPattern);
            PrintColorWriteLine(ConsoleColor.Cyan, userProductOfRoots);
            Thread.Sleep(1000);
        }

        #endregion

        #region Konfiguracja programu

        static void ExitInformation()
        {
            Console.WriteLine("\nWciśnij Enter, aby uruchomić program ponownie.");
            hint = "Wciśnięcie innego przycisku niż Enter, spowoduje zamknięcie się aplikacji.";
            PrintColorWrite(ConsoleColor.Red, hint);
        }

        static void Loading(int dots, int sleepTime, bool writeLineStart, bool writeLineEnd)
        {
            if (writeLineStart) { Console.WriteLine(); }

            Console.Write("Ładowanie");
            for (int i = 0; i < dots; i++)
            {
                Console.Write(".");
                Thread.Sleep(sleepTime);
            }

            if (writeLineEnd) { Console.WriteLine(); }
        }

        static void PrintColorWrite(ConsoleColor color, string messege)
        {
            Console.ForegroundColor = color;
            Console.Write(messege);
            Console.ResetColor();
        }

        static void PrintColorWriteLine(ConsoleColor color, string messege)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(messege);
            Console.ResetColor();
        }

        #endregion
    }
}
