using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Uchot_Finansou
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                //Предварительная инициализация переменных (Мне так удобнее)
                Random randy = new Random();
                int[] income = new int[12];
                int[] consum = new int[12];
                int[] profit = new int[12];
                int[] sortProfit = new int[12];
                int[] badMonth = new int[12];
                int count = 1;
                byte exit;
                byte bMc = 0;
                byte posi = 0;

                #region Генерация таблицы

                for (int ind = 0; ind <= 11; ind++) // Генерация и запись чисел для столбцов доходов и расходов
                {

                    income[ind] = (randy.Next(10, 40) * 10000) / 2;
                    consum[ind] = (randy.Next(14, 26) * 10000) / 2;

                    profit[ind] = income[ind] - consum[ind]; // Сразу вычисляем прибыль

                    if (profit[ind] > 0) posi++; // Подсчет кол-ва месяцев с положительной прибылью

                    // И сразу выводъ всего этого дела на экран
                    if (ind == 0)
                    {
                        WriteLine($"{"Месяц",5}{"Доход,тыс.руб.",20}{"Расход, тыс. руб.",20}{"Прибыль, тыс. руб.",20}");
                    }

                    WriteLine($"{ind + 1,5}{income[ind],19}{consum[ind],20}{profit[ind],20}");

                }

                #endregion

                #region Высчитывание плохих месяцев

                // Копирование массива прибыли для дальнейшей его обработки
                for (int ind = 0; ind <= 11; ind++)
                {
                    sortProfit[ind] = profit[ind];
                }
                Array.Sort(sortProfit); // Сортировка копии "Прибыли"

                // Замудренный процесс высчитывания того, какие месяца были самыми худшими
                for (int ind = 0; ind <= 11; ind++)
                {

                    // Проверка на з плохих месяца и их дубликатов
                    if (ind > 0)
                    {
                        if (sortProfit[ind] > sortProfit[ind - 1]) count++;
                        if (sortProfit[ind] == sortProfit[ind - 1]) continue;
                        if (count > 3) break;
                    }

                    // Запись номера плохого месяца
                    for (int ind_2 = 0; ind_2 <= 11; ind_2++)
                    {
                        if (sortProfit[ind] == profit[ind_2])
                        {
                            badMonth[bMc] = ind_2 + 1;
                            bMc++;
                        }
                    }

                }

                #endregion

                #region Вывод плохих и хороших месяцев

                Array.Sort(badMonth); // Сортировка списка плохих месяцев для красоты

                WriteLine("\nХудшая прибыль в месяцах: "); // Вывод плохих месяцевъ и их прибыли
                foreach (byte i in badMonth)
                {
                    if (i > 0)
                    {
                        WriteLine($"### Месяц: {i} / Прибыль: {profit[i - 1]} ");
                    }
                }

                WriteLine($"\nМесяцев с положительной прибылью: {posi}"); // Вывод кол-ва положительных месяцев
                WriteLine();

                #endregion

                #region Повтор или выход

                ReadKey();
                WriteLine("Запустить заново? 1 - Повтор | 0 - Выход");
                while (true) // Ввод числа и его проверка
                {
                    // Проверка введенного значения на его соответствие текущим требованиям
                    // (Кароч чтобы число не вылезло за пределы возможностей переменной и нужных нам условий)
                    if (byte.TryParse(ReadLine(), out exit) && exit <= 1 && exit >= 0) break;
                    else Write("Число должно быть целым в диапазоне от 0 до 1, попробуйте еще раз: ");
                }
                WriteLine();
                if (exit == 0) break; // Завершение программы

                #endregion
            }
        }
    }
}
