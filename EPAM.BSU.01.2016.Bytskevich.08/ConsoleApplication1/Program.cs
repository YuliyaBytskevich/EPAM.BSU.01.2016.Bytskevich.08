using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Task1.GenericMatirx;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ElementChangedHandler handlerForSquareM = new ElementChangedHandler();
            ElementChangedHandler handlerForDiagM = new ElementChangedHandler();
            ElementChangedHandler handlerForSymmM = new ElementChangedHandler();
            SquareMatrix<int> squareM = new SquareMatrix<int>(3, handlerForSquareM);
            squareM.SetCellValue(0, 0, 10);
            squareM.SetCellValue(1, 0, -5);
            squareM.SetCellValue(0, 1, -3);
            Console.WriteLine(squareM);
            DiagonalMatrix<int> diagM = new DiagonalMatrix<int>(5, handlerForSquareM);
            diagM.SetCellValue(0,0, 1);
            diagM.SetCellValue(1, 1, 2);
            diagM.SetCellValue(1, 3, 3);
            Console.WriteLine(diagM);
            SymmetricMatrix<int> symmM = new SymmetricMatrix<int>(5, handlerForSymmM);
            symmM.SetCellValue(0,0, 1);
            symmM.SetCellValue(1, 1, 2);
            symmM.SetCellValue(0,1, 10);
            symmM.SetCellValue(2, 0, 20);
            Console.WriteLine(symmM);

            SquareMatrix<int> squareSum = squareM + diagM + symmM;
            Console.WriteLine(squareSum);

            SquareMatrix<string> first = new SquareMatrix<string>(5, handlerForSquareM);
            first.SetCellValue(3,3,"hello");
            first.SetCellValue(3, 4, "world");
            Console.WriteLine(first);

            SquareMatrix<string> second = new SquareMatrix<string>(5, handlerForSquareM);
            second.SetCellValue(1, 1, "FUCK!");
            Console.WriteLine(second);

            SquareMatrix<string> third = first + second;
            Console.WriteLine(third);*/

           

            ElementChangedHandler<EventArgs> handlerForSquareM = new ElementChangedHandler<EventArgs>();
            SquareMatrix<int, EventArgs> squareM = new SquareMatrix<int, EventArgs>(3);
            squareM.EnableHandlingOnChanging(handlerForSquareM);
            squareM.AddCustomEventOnChanging(WriteSomething);
            squareM.SetCellValue(0, 0, 10);
            squareM.SetCellValue(1, 0, -5);
            squareM.DisableHandlingOnChanging();
            squareM.SetCellValue(0, 1, -3);
            Console.WriteLine(squareM);



            Console.ReadKey();
        }

        public static void WriteSomething(Object sender, EventArgs e)
        {
            Console.WriteLine("changing some element");
            
        }


    }
}
