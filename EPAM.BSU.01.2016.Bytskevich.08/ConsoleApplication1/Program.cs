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
            SquareMatrix<int> squareM = new SquareMatrix<int>(3);
            squareM.SetCellValue(0, 0, 10);
            squareM.SetCellValue(1, 0, -5);
            squareM.SetCellValue(0, 1, -3);
            Console.WriteLine(squareM);
            DiagonalMatrix<int> diagM = new DiagonalMatrix<int>(5);
            diagM.SetCellValue(0,0, 1);
            diagM.SetCellValue(1, 1, 2);
            diagM.SetCellValue(1, 3, 3);
            Console.WriteLine(diagM);
            SymmetricMatrix<int> symmM = new SymmetricMatrix<int>(5);
            symmM.SetCellValue(0,0, 1);
            symmM.SetCellValue(1, 1, 2);
            symmM.SetCellValue(0,1, 10);
            symmM.SetCellValue(2, 0, 20);
            Console.WriteLine(symmM);

            SquareMatrix<int> squareSum = squareM + diagM + symmM;
            Console.WriteLine(squareSum);

            Console.ReadKey();
        }
    }
}
