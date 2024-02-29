using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Linq.Expressions;
using System.Dynamic;
using System.Linq.Dynamic.Core;

namespace MetodosCerrados
{
    public class Funciones
    {
        public static void Main(string []args)
        {
            StreamWriter archivo = new StreamWriter("Operaciones.log");
            string operacion = "", resultado = "";
            double x0 = 0, x1 = 1, erpa = 100;
            // Leer expresiones de la consola
            Func<double, double> fu = x => Math.Pow(x,3) + (2 * Math.Pow(x,2))  - 1;
            Console.WriteLine(fu(0));
            Console.WriteLine(f(0));
            Console.WriteLine("\t\tPruebas de lectura de funciones");
            operacion = Console.ReadLine();
            archivo.Write(operacion);
            archivo.Close();
            using(TokenA L = new TokenA())
            {
                while(!L.finArchivo())
                {
                    L.nextToken();
                    resultado += L.getContenido();
                }
            }
            Console.WriteLine("Funcion: " + resultado);

            // Convertir la cadena de texto ingresada por el usuario ya convertida en una expresión lambda
            ParameterExpression x = Expression.Parameter(typeof(double), "x");
            Expression<Func<double, double>> funcionExp = (Expression<Func<double, double>>)System.Linq.Dynamic.Core.DynamicExpressionParser.ParseLambda(new[] { x }, null, resultado);

            // Compilar la expresión lambda
            Func<double, double> fi = funcionExp.Compile();

            Console.WriteLine(fi(0));

            Console.ReadKey();
            // solo el metodo de la secante
            Console.WriteLine("\t\tPruebas de raiz de funciones");
            Console.WriteLine("\tIngresa el valor de x0: ");
            x0 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("\tIngresa el valor de x1: ");
            x1 = Convert.ToDouble(Console.ReadLine());
            while(Math.Abs(erpa) > 0.050f)
            {
                double aux;
                aux = x1;
                x1 = xi_plusone(x1,x0);
                x0 = aux;
                erpa = ErrorRelativoPorcentualAproximado(x1,x0);
            }
            Console.WriteLine("Valor de la raiz final: " + f(x1) + ", en " + x1);
        }
        // f(x)
        static public double f(double x)
        {
            return Math.Pow(x,3) + (2 * Math.Pow(x,2))  - 1;
        }
        // xi + 1
        static public double xi_plusone(double xi, double ximinusone)
        {
            return xi - (f(xi)*(ximinusone - xi))/(f(ximinusone) - f(xi));
        }
        // ea entre los dos valores aproximados
        static public double ErrorRelativoPorcentualAproximado(double xi, double ximinusone)
        {
            return ((Math.Abs(xi - ximinusone) / xi) * 100);
        }
    }
}