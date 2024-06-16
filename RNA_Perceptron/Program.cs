using System;
using System.Data;
using System.Numerics;

class Principal
{
    

    static void Main()
    {
        int[] X1 = new int[3];
        int[] X2 = new int[3];
        int[] X3 = new int[3];
        int[] Yk = new int[3];
        int[] W1 = new int[3];
        int[] W2 = new int[3];
        int[] W3 = new int[3];
        int[] WBias = new int[3];
        int Bias, TaxaAprendizagem;
        char respostaBias, respostaEntrada;

        Inicio();

        do
        {
            for (int entrada = 0, auxiliar = 0; entrada <= 11; entrada++)
            {
                if (entrada == 4 || entrada == 8 || entrada == 4) { auxiliar++; }

                Console.Write(InserirValores(entrada));
                if (entrada == 0 || entrada == 4 || entrada == 8)
                {
                    X1[auxiliar] = int.Parse(Console.ReadLine());
                }
                else if (entrada == 1 || entrada == 5 || entrada == 9)
                {
                    X2[auxiliar] = int.Parse(Console.ReadLine());
                }
                else if (entrada == 2 || entrada == 6 || entrada == 10)
                {
                    X3[auxiliar] = int.Parse(Console.ReadLine());
                }
                else
                {
                    Yk[auxiliar] = int.Parse(Console.ReadLine());
                }
            }
            TabelaEntradas(X1, X2, X3, Yk);
            Console.WriteLine("Deseja cofirmar as suas entradas?[S/N]");
            respostaEntrada = char.Parse(Console.ReadLine());
            Console.Clear();
        } while (respostaEntrada == 'N' || respostaEntrada == 'n');
        

        Console.WriteLine("Deseja utilizar o Bias?[S/N]");
        respostaBias = char.Parse(Console.ReadLine());

        if(respostaBias == 'S' || respostaBias == 's') {
            Bias = 1;
        }

    }

  
    static void Inicio()
    {
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("Inicio - RNA Perceptron");
        Console.WriteLine("---------------------------------------------");
    }

    static string InserirValores(int x)
    {
        string[] entradas = {
            "Insira X1: ", // 0
            "Insira X2: ", // 1
            "Insira X3: ", // 2
            "Insira Yk: ", // 3
            "Insira X1: ", // 4
            "Insira X2: ", // 5
            "Insira X3: ", // 6
            "Insira Yk: ", // 7
            "Insira X1: ", // 8
            "Insira X2: ", // 9
            "Insira X3: ", // 10
            "Insira Yk: "  // 11
        };

        if (x >= 0 && x < entradas.Length){
            return entradas[x];
        }
        return "Entrada Inválida";
    }

    static void TabelaEntradas(int[] x1, int[] x2, int[] x3, int[] yk)
    {
        Console.Clear();
        Console.WriteLine("Tabela de entrada:");
        Console.WriteLine("-------------------------------------");
        Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", "X1", "X2", "X3", "YK");
        Console.WriteLine("_____________________________________");
        for (int i = 0; i < x1.Length; i++)
        {
            Console.WriteLine("{0,-10} {1,-10} {2,-10} {3,-10}", x1[i], x2[i], x3[i], yk[i]);
            Console.WriteLine("-------------------------------------");
        }
        
    }
}
