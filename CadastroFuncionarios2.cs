using System;
using System.IO;
using System.Collections.Generic;

namespace CadastroFuncionario
{
    class Program
    {
        class Funcionario
        {
            public char Status;
            public short Matricula;
            public string Nome, Funcao, Setor;
            public double Salario;

            public Funcionario()
            {
                Status = ' ';
                Matricula = 0;
                Nome = "";
                Funcao = "";
                Setor = "";
                Salario = 0.0;
            }

            public void Salvar(BinaryWriter BW)
            {
                BW.Write(Status);
                BW.Write(Matricula);
                BW.Write(Nome);
                BW.Write(Funcao);
                BW.Write(Setor);
                BW.Write(Salario);
            }

            public void Ler(BinaryReader BR)
            {
                Status = BR.ReadChar();
                Matricula = BR.ReadInt16();
                Nome = BR.ReadString();
                Funcao = BR.ReadString();
                Setor = BR.ReadString();
                Salario = BR.ReadDouble();
            }
        }

        static string NomeArq = "Func.dat";
        static string NomeABP = "FuncABP.dat";
        static FileStream streamArq;
        static BinaryWriter outFunc;
        static BinaryReader inFunc;
        // ABP
        static FileStream streamABP;
        static BinaryWriter outFuncABP;
        static BinaryReader inFuncABP;

        static void Inicializar()
        {
            streamArq = new FileStream(NomeArq, FileMode.OpenOrCreate, FileAccess.ReadWrite);            
            outFunc = new BinaryWriter(streamArq);
            inFunc = new BinaryReader(streamArq);

            streamABP = new FileStream(NomeABP, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            outFuncABP = new BinaryWriter(streamABP);
            inFuncABP = new BinaryReader(streamABP);
        }

        static void Finalizar()
        {
            outFunc.Close();
            inFunc.Close();
            streamArq.Close();
        }

        static void MenuPrincipal()
        {
            byte opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("CADASTRO DE FUNCIONÁRIOS");
                Console.WriteLine("========================\n");
                Console.WriteLine("1 - Cadastrar funcionário");
                Console.WriteLine("2 - Listar funcionários");
                Console.WriteLine("3 - Pesquisar funcionário");
                Console.WriteLine("4 - Excluir funcionário (exclusão lógica)");
                Console.WriteLine("5 - Excluir funcionário (exclusão física)");
                Console.WriteLine("6 - Alterar funcionário (alteração lógica)");
                Console.WriteLine("7 - Alterar funcionário (alteração física)");
                Console.WriteLine("8 - Gerar índices");
                Console.WriteLine("9 - Listar arquivo completo");
                Console.WriteLine("10 - Eliminar registros excluídos logicamente (Compactar)");
                Console.WriteLine("11 - Listar pelo índice de matrícula");
                Console.WriteLine("12 - Listar pelo índice de nome");
                Console.WriteLine("13 - Pesquisar funcionário (índice de matrícula)");
                Console.WriteLine("14 - Pesquisar funcionário (Pesquisa binária pelo índice de matrícula)");
                Console.WriteLine("15 - Sair\n");
                Console.Write("Digite sua opção : ");
                opcao = byte.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        CadastrarFuncionario();
                        break;
                    case 2:
                        ListarFuncionarios();
                        break;
                    case 3:
                        PesquisarFuncionario();
                        break;
                    case 4:
                        ExcluirFuncionarioLogico();
                        break;
                    case 5:
                        ExcluirFuncionarioFisico();
                        break;
                    case 6:
                        AlterarFuncionarioLogico();
                        break;
                    case 7:
                        break;
                    case 8:
                        GerarIndices();
                        break;
                    case 9:
                        ListarArquivo();
                        break;
                    case 10:
                        Compactar();
                        break;
                    case 11:
                        ListaIdxMat();
                        break;
                    case 12:
                        ListaIdxNome();
                        break;
                    case 13:
                        PesquisaIdxMat();
                        break;
                    case 14:
                        PesquisaBinIdxMat();
                        break;
                    case 15:
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            } while (opcao >= 1 && opcao <= 14);
        }

        static void CadastrarFuncionario()
        {
            streamArq.Seek(streamArq.Length, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();

            Console.Clear();
            Console.Write("Matrícula.: ");
            Func.Matricula = short.Parse(Console.ReadLine());
            Console.Write("Nome......: ");
            Func.Nome = Console.ReadLine();
            Console.Write("Função....: ");
            Func.Funcao = Console.ReadLine();
            Console.Write("Setor.....: ");
            Func.Setor = Console.ReadLine();
            Console.Write("Salário...: R$ ");
            Func.Salario = double.Parse(Console.ReadLine());
            Func.Salvar(outFunc);
        }

        static void ListarFuncionarios()
        {
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            int cont = 0, Pag=1;
            Console.Clear();
            Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA "+Pag+")");
            Console.WriteLine("===================================\n");
            while (inFunc.PeekChar()>=0)
            {
                Func.Ler(inFunc);
                if (Func.Status != '*')
                {
                    Console.WriteLine("Matrícula : " + Func.Matricula + "\t\tNome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                    cont++;
                    if (cont == 6)
                    {
                        Pag++;
                        cont = 0;
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
                        Console.WriteLine("===================================\n");
                    }
                }
            }
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void ListarArquivo()
        {
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            int cont = 0, Pag = 1;
            Console.Clear();
            Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
            Console.WriteLine("===================================\n");
            while (inFunc.PeekChar() >= 0)
            {
                Func.Ler(inFunc);
                    Console.WriteLine("Status = "+Func.Status+"\tMatrícula : " + Func.Matricula + "\t\tNome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                    cont++;
                    if (cont == 6)
                    {
                        Pag++;
                        cont = 0;
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
                        Console.WriteLine("===================================\n");
                    }
                
            }
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void PesquisarFuncionario()
        {
            Console.Clear();
            Console.WriteLine("PESQUISA DE FUNCIONÁRIOS (POR MATRÍCULA)");
            Console.WriteLine("========================================\n");
            streamArq.Seek(0, SeekOrigin.Begin);
            short Matricula;
            bool achou = false;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            Matricula = short.Parse(Console.ReadLine());
            while (!achou && inFunc.PeekChar() >= 0)
            {
                Func.Ler(inFunc);
                if (Func.Status != '*')
                {
                    if (Func.Matricula == Matricula)
                    {
                        Console.WriteLine("\nNome : " + Func.Nome);
                        Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                        Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                        achou = true;
                    }
                }
            }
            if (!achou)
                Console.WriteLine("Funcionário não localizado.\n");
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void ExcluirFuncionarioLogico()
        {
            Console.Clear();
            Console.WriteLine("EXCLUSÃO DE FUNCIONÁRIOS (POR MATRÍCULA)");
            Console.WriteLine("========================================\n");
            streamArq.Seek(0, SeekOrigin.Begin);
            short Matricula;
            bool achou = false;
            long ultimaPos = 0;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            Matricula = short.Parse(Console.ReadLine());
            while (inFunc.PeekChar() >= 0)
            {
                ultimaPos = streamArq.Position;
                Func.Ler(inFunc);
                if (Func.Matricula == Matricula)
                {
                    Console.WriteLine("\nNome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");

                    Console.Write("Confirma a exclusão (S|N) ? ");
                    char op = char.Parse(Console.ReadLine().ToUpper());
                    if (op == 'S')
                    {
                        Func.Status = '*';
                        streamArq.Seek(ultimaPos, SeekOrigin.Begin);
                        Func.Salvar(outFunc);
                        Console.WriteLine("Funcionário excluído com sucesso.");
                    }
                    else
                        Console.WriteLine("Exclusão cancelada.");
                    achou = true;
                }
            }
            if (!achou)
                Console.WriteLine("Funcionário não localizado.\n");
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void ExcluirFuncionarioFisico()
        {
            BinaryWriter temp = new BinaryWriter(new FileStream("temp.dat", FileMode.Create, FileAccess.Write));

            Console.Clear();
            Console.WriteLine("EXCLUSÃO DE FUNCIONÁRIOS (POR MATRÍCULA)");
            Console.WriteLine("========================================\n");
            streamArq.Seek(0, SeekOrigin.Begin);
            short Matricula;
            bool achou = false;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            Matricula = short.Parse(Console.ReadLine());
            while (inFunc.PeekChar() >= 0)
            {
                Func.Ler(inFunc);
                if (Func.Matricula != Matricula)
                    Func.Salvar(temp);
                else
                    achou = true;
            }
            temp.Close();
            if (achou)
            {
                Finalizar();
                File.Delete(NomeArq);
                File.Move("temp.dat", NomeArq);
                Console.WriteLine("Funcionário excluído com sucesso.\n");
                Inicializar();
            }
            else
            {
                File.Delete("temp.dat");
                Console.WriteLine("Funcionário não localizado.\n");
            }
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void AlterarFuncionarioLogico()
        {
            Console.Clear();
            Console.WriteLine("ALTERAÇÃO DE FUNCIONÁRIOS (POR MATRÍCULA)");
            Console.WriteLine("========================================\n");
            streamArq.Seek(0, SeekOrigin.Begin);
            short Matricula;
            bool achou = false;
            long ultimaPos = 0;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            Matricula = short.Parse(Console.ReadLine());
            while (!achou && inFunc.PeekChar() >= 0)
            {
                ultimaPos = streamArq.Position;
                Func.Ler(inFunc);
                if (Func.Status != '*')
                {
                    if (Func.Matricula == Matricula)
                    {
                        Console.WriteLine("\nNome : " + Func.Nome);
                        Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                        Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                        achou = true;
                    }
                }
            }
            if (achou)
            {
                Funcionario FuncAux = new Funcionario();
                char Confirma = 'S';

                Console.Write("Nome......: ");
                FuncAux.Matricula = Func.Matricula;
                FuncAux.Nome = Console.ReadLine();
                if (FuncAux.Nome == "")
                    FuncAux.Nome = Func.Nome;
                Console.Write("Função....: ");
                FuncAux.Funcao = Console.ReadLine();
                if (FuncAux.Funcao == "")
                    FuncAux.Funcao = Func.Funcao;
                Console.Write("Setor.....: ");
                FuncAux.Setor = Console.ReadLine();
                if (FuncAux.Setor == "")
                    FuncAux.Setor = Func.Setor;
                Console.Write("Salário...: R$ ");
                FuncAux.Salario = double.Parse(Console.ReadLine());
                if (FuncAux.Salario == 0)
                    FuncAux.Salario = Func.Salario;

                Console.WriteLine("\nNome : " + FuncAux.Nome);
                Console.WriteLine("Função : " + FuncAux.Funcao + "\tSetor : " + FuncAux.Setor);
                Console.WriteLine("Salário : R$ " + FuncAux.Salario + "\n");
                Console.Write("Confirma a alteração dos dados (S|N) ? ");
                Confirma = char.Parse(Console.ReadLine().ToUpper());
                if (Confirma == 'S')
                {
                    Func.Status = '*';
                    streamArq.Seek(ultimaPos, SeekOrigin.Begin);
                    Func.Salvar(outFunc);
                    streamArq.Seek(streamArq.Length, SeekOrigin.Begin);
                    FuncAux.Salvar(outFunc);
                }
            }
            else
                Console.WriteLine("Funcionário não localizado.\n");

            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
            //inFunc.Close();
        }

        static void GerarIndices()
        {            
            SortedDictionary<short, long> dicMatricula = new SortedDictionary<short, long>();
            SortedDictionary<string, long> dicNome = new SortedDictionary<string, long>();
            BinaryWriter idxMat = new BinaryWriter(new FileStream("FuncMat.idx", FileMode.Create, FileAccess.Write));
            BinaryWriter idxNome = new BinaryWriter(new FileStream("FuncNome.idx", FileMode.Create, FileAccess.Write));
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            long ultimaPos = 0;
            while (inFunc.PeekChar() >= 0)
            {
                ultimaPos = streamArq.Position;
                Func.Ler(inFunc);
                if (Func.Status != '*')
                {
                    dicMatricula.Add(Func.Matricula, ultimaPos);
                    dicNome.Add(Func.Nome, ultimaPos);
                }
            }
            foreach (KeyValuePair<short, long> kvp in dicMatricula)
            {
                idxMat.Write(kvp.Key);
                idxMat.Write(kvp.Value);
            }
            foreach (KeyValuePair<string, long> kvp in dicNome)
            {
                idxNome.Write(kvp.Key);
                idxNome.Write(kvp.Value);
            }
            idxMat.Close();
            idxNome.Close();
        }

        static void Compactar()
        {
            BinaryWriter temp = new BinaryWriter(new FileStream("temp.dat", FileMode.Create, FileAccess.Write));
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            while (inFunc.PeekChar() >= 0)
            {
                Func.Ler(inFunc);
                if(Func.Status != '*')
                    Func.Salvar(temp);
            }
            temp.Close();
            Finalizar();
            File.Delete(NomeArq);
            File.Move("temp.dat", NomeArq);
            Inicializar();
        }

        static void ListaIdxMat()
        {
            BinaryReader idxMat = new BinaryReader(new FileStream("FuncMat.idx", FileMode.Open, FileAccess.Read));
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            int cont = 0, Pag = 1;
            short Matr;
            long Pos = 0;
            Console.Clear();
            Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
            Console.WriteLine("===================================\n");
            while (idxMat.PeekChar() >= 0)
            {
                Matr = idxMat.ReadInt16();
                Pos = idxMat.ReadInt64();
                streamArq.Seek(Pos, SeekOrigin.Begin);
                Func.Ler(inFunc);
                    Console.WriteLine("Matrícula : " + Func.Matricula + "\t\tNome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                    cont++;
                    if (cont == 6)
                    {
                        Pag++;
                        cont = 0;
                        Console.WriteLine("Pressione qualquer tecla...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
                        Console.WriteLine("===================================\n");
                    }
            }
            idxMat.Close();
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void ListaIdxNome()
        {
            BinaryReader idxNome = new BinaryReader(new FileStream("FuncNome.idx", FileMode.Open, FileAccess.Read));
            streamArq.Seek(0, SeekOrigin.Begin);
            Funcionario Func = new Funcionario();
            int cont = 0, Pag = 1;
            string Nome;
            long Pos = 0;
            Console.Clear();
            Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
            Console.WriteLine("===================================\n");
            while (idxNome.PeekChar() >= 0)
            {
                Nome = idxNome.ReadString();
                Pos = idxNome.ReadInt64();
                streamArq.Seek(Pos, SeekOrigin.Begin);
                Func.Ler(inFunc);
                Console.WriteLine("Matrícula : " + Func.Matricula + "\t\tNome : " + Func.Nome);
                Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                cont++;
                if (cont == 6)
                {
                    Pag++;
                    cont = 0;
                    Console.WriteLine("Pressione qualquer tecla...");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine("LISTAGEM DE FUNCIONÁRIOS (PÁGINA " + Pag + ")");
                    Console.WriteLine("===================================\n");
                }
            }
            idxNome.Close();
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void PesquisaIdxMat()
        {
            Console.Clear();
            Console.WriteLine("PESQUISA DE FUNCIONÁRIOS (PELO ÍNDICE DE MATRÍCULA)");
            Console.WriteLine("===================================================\n");
            BinaryReader idxMat = new BinaryReader(new FileStream("FuncMat.idx", FileMode.Open, FileAccess.Read));
            short MatrPesq, Matr;
            long Pos;
            bool achou = false;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            MatrPesq = short.Parse(Console.ReadLine());
            while (!achou && idxMat.PeekChar() >= 0)
            {
                Matr = idxMat.ReadInt16();
                Pos = idxMat.ReadInt64();
                if (MatrPesq == Matr)
                {
                    streamArq.Seek(Pos, SeekOrigin.Begin);
                    Func.Ler(inFunc);
                    Console.WriteLine("\nFuncionário localizado.\n");
                    Console.WriteLine("Nome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                    achou = true;
                }
            }
            if (!achou)
                Console.WriteLine("\nFuncionário não localizado.\n");
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void PesquisaBinIdxMat()
        // A pesquisa binária só é possível quando os registros (seja no arquivo de dados ou índice) tem tamanho fixo
        {
            Console.Clear();
            Console.WriteLine("PESQUISA DE FUNCIONÁRIOS (PELO ÍNDICE DE MATRÍCULA)");
            Console.WriteLine("===================================================\n");
            BinaryReader idxMat = new BinaryReader(new FileStream("FuncMat.idx", FileMode.Open, FileAccess.Read));
            short MatrPesq, Matr;
            long Pos, //Posição do registro no arquivo de dados
                 inicio = 0, // posição (RRN) do primeiro registro do intervalo a ser pesquisado
                 fim, // posição (RRN) do último registro do intervalo a ser pesquisado
                 meio, // posição (RRN) do registro central do intervalo a ser pesquisado
                 tamRegistro; // Tamanho dos registros no arquivo de índice
            tamRegistro = sizeof(short)+sizeof(long);
            fim = (idxMat.BaseStream.Length)/tamRegistro-1;
            meio = (inicio + fim) / 2;
            bool achou = false;
            Funcionario Func = new Funcionario();
            Console.Write("Matrícula : ");
            MatrPesq = short.Parse(Console.ReadLine());
            while (fim >= inicio && !achou)
            {
                idxMat.BaseStream.Seek(meio * tamRegistro, SeekOrigin.Begin);
                Matr = idxMat.ReadInt16();
                Pos = idxMat.ReadInt64();
                Console.WriteLine("Matrícula atual: " + Matr);
                if (MatrPesq == Matr)
                {
                    streamArq.Seek(Pos, SeekOrigin.Begin);
                    Func.Ler(inFunc);
                    Console.WriteLine("\nFuncionário localizado.\n");
                    Console.WriteLine("Nome : " + Func.Nome);
                    Console.WriteLine("Função : " + Func.Funcao + "\tSetor : " + Func.Setor);
                    Console.WriteLine("Salário : R$ " + Func.Salario + "\n");
                    achou = true;
                }
                else
                {
                    if (MatrPesq < Matr)
                        fim = meio - 1;
                    else
                        inicio = meio + 1;
                    meio = (inicio + fim) / 2;
                }
            }
            if (!achou)
                Console.WriteLine("\nFuncionário não localizado.\n");
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Inicializar();
            MenuPrincipal();
            Finalizar();
        }

    }
}