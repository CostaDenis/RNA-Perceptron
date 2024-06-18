using System;
using System.Data;
using System.Numerics;
class Principal
{
    static void Main()
    {
        //Valor das entradas e Resultado Esperado
        float[] X1 = new float[3];
        float[] X2 = new float[3];
        float[] X3 = new float[3];
        float[] Yk = new float[3];
        //Pesos
        float[] W1 = new float[3];
        float[] W2 = new float[3];
        float[] W3 = new float[3];
        float[] WBias = new float[3];
        //Taxa de Aprendizagem
        float TaxaAprendizagem = 0.0f;
        //Variáveis complementares
        int Bias;
        char resposta;
        bool EncerrarPrograma = false;
        //Peso Encontrado
        float[] WF = new float[4];
        //Total de Interações necessárias
        int QTDEIteracoes = 0;

        Inicio();

        do
        {
            do
            {
                for (int entrada = 0, auxiliar = 0; entrada <= 11; entrada++)
                {
                    if (entrada == 4 || entrada == 8) { auxiliar++; }

                    Console.Write(InserirValores(entrada));
                    if (entrada == 0 || entrada == 4 || entrada == 8) {
                        X1[auxiliar] = float.Parse(Console.ReadLine());
                    } else if (entrada == 1 || entrada == 5 || entrada == 9) {
                        X2[auxiliar] = float.Parse(Console.ReadLine());
                    }
                    else if (entrada == 2 || entrada == 6 || entrada == 10) {
                        X3[auxiliar] = float.Parse(Console.ReadLine());
                    } else {
                        Yk[auxiliar] = float.Parse(Console.ReadLine());
                    }
                }
                TabelaEntradas(X1, X2, X3, Yk);
                Console.WriteLine("\nDeseja cofirmar as suas entradas?[S/N]");
                resposta = char.Parse(Console.ReadLine());
                Console.Clear();

            } while (char.ToUpper(resposta) == 'N');

            Console.WriteLine("Deseja utilizar o Bias?[S/N]");
            resposta = char.Parse(Console.ReadLine());
            Bias = UsarBias(resposta);
            resposta = ' ';

            do {
                if (WF[0] == 0.0f) {
                    Console.WriteLine("Deseja inserir os primeiros pesos?[S/N]");
                    Console.WriteLine("(Caso não queira, todos os pesos inicias irão valer 0,2)");
                } else {
                    Console.WriteLine("[P]Deseja inserir os primeiros pesos");
                    Console.WriteLine("[R]Todos os pesos inicias irão valer 0,2");
                    Console.WriteLine("[T]Utilizar pesos aprendidos anteriormente");
                }
                resposta = char.Parse(Console.ReadLine());
            } while (!VerificarOpcaoPeso(char.ToUpper(resposta)));
           

            if (char.ToUpper(resposta) == 'N' || char.ToUpper(resposta) == 'R') {
                W1[0] = 0.2f;
                W2[0] = 0.2f;
                W3[0] = 0.2f;
                WBias[0] = 0.2f;
            } else if (char.ToUpper(resposta) == 'S' || char.ToUpper(resposta) == 'P'){
                Console.WriteLine("Digite o valor do W1: ");
                W1[0] = float.Parse(Console.ReadLine());
                Console.WriteLine("Digite o valor do W2: ");
                W2[0] = float.Parse(Console.ReadLine());
                Console.WriteLine("Digite o valor do W3: ");
                W3[0] = float.Parse(Console.ReadLine());
                Console.WriteLine("Digite o valor do WBias: ");
                WBias[0] = float.Parse(Console.ReadLine());
            } else {
                W1[0] = WF[0];
                W2[0] = WF[1];
                W3[0] = WF[2];
                WBias[0] = WF[3];
            }

            Console.WriteLine("Insira a Taxa de Aprendizagem: ");
            TaxaAprendizagem = float.Parse(Console.ReadLine());

            WF = IteracaoPerceptron(X1, X2, X3, Bias, W1, W2, W3, WBias, Yk, TaxaAprendizagem, out QTDEIteracoes);

            Console.WriteLine("\n----------------------------------------------------");
            Console.Write("\nPesos encontrados:");
            foreach (float i in WF) {
                Console.Write($"\t{i.ToString("F2")}");
            }
            Console.WriteLine("\nQuantidade de Iterações: " + QTDEIteracoes);
            Console.WriteLine("\n----------------------------------------------------");


            Console.WriteLine("\nDeseja testar novamente?[S/N]");
            resposta = char.Parse(Console.ReadLine());
            EncerrarPrograma = Questao(resposta);
            Console.Clear();
        } while(EncerrarPrograma);
       
    }

    static void Inicio()
    {
        Console.WriteLine("---------------------------------------------");
        Console.WriteLine("Inicio - RNA Perceptron");
        Console.WriteLine("---------------------------------------------");
    }

    static bool Questao(char r)
    {
        bool resultado;
        resultado = (r == 'S' || r == 's') ? true : false;
        return resultado; 
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

    static int UsarBias(char c)
    {
        int resultado = (c == 'S' || c == 's') ? 1 : 0;
        return resultado;
    }

    static bool VerificarOpcaoPeso(char c)
    {
        if (c == 'N' || c == 'R' || c == 'S' || c == 'T' || c == 'P') { return true; }
        return false;
    }

    static void TabelaEntradas(float[] x1, float[] x2, float[] x3, float[] yk)
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

    static float[] IteracaoPerceptron(float[] x1, float[] x2, float[] x3, float bias, float[] w1, float[] w2, float[] w3, float[] wbias, float[] yk, float TaxaAprendizagem, out int QtdeIteracao) 
    {
        float fnet = 0.0f, y = 0.0f, diferenca = 0.0f;
        float DW1 = 0.0f, DW2 = 0.0f, DW3 = 0.0f, DWBias = 0.0f;
        bool Continuar = true;
        int Iteracao = 0;

        Console.Clear();
        while (Continuar)
        {
            float TotalDeltas = 0f, TotalDiferencas = 0f;
            Continuar = false;

            Console.WriteLine();
            Console.WriteLine($"Tabela Interação número {Iteracao + 1}");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("{0,-6} {1,-6} {2,-6} {3,-8} {4,-6} {5,-6} {6,-6} {7,-10} {8,-8} {9,-10} {10,-8} {11,-10} {12,-8} {13,-8} {14,-8} {15,-10}", "X1", "X2", "X3", "Bias", "W1", "W2", "W3", "WBias", "V.E.", "f(Net)", "Y", "Diferença", "DW1", "DW2", "DW3", "DWBias");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");

            //ITERAÇÃO
            for (int i = 0; i < 3; i++)
            {
                if (i == 0 && Iteracao != 0) {
                    w1[i] = w1[i + 2] + DW1;
                    w2[i] = w2[i + 2] + DW2;
                    w3[i] = w3[i + 2] + DW3;
                    wbias[i] = wbias[i + 2] + DWBias;
                } else if(i != 0) {
                    w1[i] = w1[i - 1] + DW1;
                    w2[i] = w2[i - 1] + DW2;
                    w3[i] = w3[i - 1] + DW3;
                    wbias[i] = wbias[i - 1] + DWBias;
                } else {
                    w1[i] += DW1;
                    w2[i] += DW2;
                    w3[i] += DW3;
                    wbias[i] += DWBias;
                }

                //Fnet
                fnet = (x1[i] * w1[i]) + (x2[i] * w2[i]) + (x3[i] * w3[i]) + (bias * wbias[i]);

                //Y
                y = fnet > 0.0f ? 1.0f : 0.0f;

                //Diferença
                diferenca = yk[i] - y;
                //Deltas
                DW1 = TaxaAprendizagem * x1[i] * diferenca;
                DW2 = TaxaAprendizagem * x2[i] * diferenca;
                DW3 = TaxaAprendizagem * x3[i] * diferenca;
                DWBias = TaxaAprendizagem * bias * diferenca;

                TotalDiferencas += Math.Abs(diferenca);
                TotalDeltas += Math.Abs(DW1) + Math.Abs(DW2) + Math.Abs(DW3) + Math.Abs(DWBias);

                Console.WriteLine("{0,-6} {1,-6} {2,-6} {3,-8} {4,-6} {5,-6} {6,-6} {7,-10} {8,-8} {9,-10} {10,-8} {11,-10} {12,-8} {13,-8} {14,-8} {15,-10}", x1[i], x2[i], x3[i], bias, w1[i].ToString("F2"), w2[i].ToString("F2"), w3[i].ToString("F2"), wbias[i].ToString("F2"), yk[i], fnet.ToString("F2"), y, diferenca, Math.Abs(DW1).ToString("F2"), DW2.ToString("F2"), DW3.ToString("F2"), DWBias.ToString("F2"));
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------");

                if(TotalDiferencas != 0 || TotalDeltas != 0) {
                    Continuar = true;
                }
            }
            Iteracao++;
        }
        float[] WF = new float[4] { w1[0], w2[0], w3[0], wbias[0] };
        QtdeIteracao = Iteracao;
        return WF;
    }
}
