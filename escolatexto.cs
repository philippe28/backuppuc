using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Universidade
{
    class Aluno
    {
        public char Status;
        public short Matricula, Periodo, Dia, Mes;
        public int Ano;
        public long Telefone;
        public string Nome, Curso, Rua, Bairro, Cidade, Estado;

        public Aluno()
        {
            Status = ' ';
            Matricula = 0;
            Curso = "";
            Periodo = 0;
            Nome = "";
            Rua = "";
            Bairro = "";
            Cidade = "";
            Estado = "";
            Dia = 0;
            Mes = 0;
            Ano = 0;
            Telefone = 0;
        }

        public void Salvar(BinaryWriter BW)
        {
            BW.Write(Status);
            BW.Write(Matricula);
            BW.Write(Curso);
            BW.Write(Periodo);
            BW.Write(Nome);
            BW.Write(Rua);
            BW.Write(Bairro);
            BW.Write(Cidade);
            BW.Write(Estado);
            BW.Write(Dia);
            BW.Write(Mes);
            BW.Write(Ano);
            BW.Write(Telefone);
        }

        public void Ler(BinaryReader BR)
        {
            Status = BR.ReadChar();
            Matricula = BR.ReadInt16();
            Curso = BR.ReadString();
            Periodo = BR.ReadInt16();
            Nome = BR.ReadString();
            Rua = BR.ReadString();
            Bairro = BR.ReadString();
            Cidade = BR.ReadString();
            Estado = BR.ReadString();
            Dia = BR.ReadInt16();
            Mes = BR.ReadInt16();
            Ano = BR.ReadInt32();
            Telefone = BR.ReadInt64();
        }

    }
    class Program
    {
        static string data, hora;
        static string NomeArq = "Aluno.dat";
        static string NomeAux = "Auxiliar.dat";
        static string inm = "AlNome.idx";
        static string imt = "AlMat.idx";
        static string relatorio = "Relatorio_Curso.txt";
        static string Aluno = "Relatorio_Aluno.txt";
        static string rel_SI = "Relatorio_SI.txt";
        static FileStream Arquivo;
        static BinaryWriter Escreve_Arquivo;
        static BinaryReader Ler_Arquivo;
        static FileStream Auxiliar;
        static BinaryWriter Aux;
        static BinaryReader Ler_Aux;
        static FileStream Substituta;
        static BinaryWriter Subs;
        static BinaryReader Subs_ler;
        static FileStream IMat;
        static BinaryWriter idxMat;
        static BinaryReader Ler_idxMat; 
        static FileStream Inome;
        static BinaryWriter idxNome;
        static BinaryReader Ler_idxNome;
        static FileStream Relatorio;
        static StreamWriter Escreve_Relatorio;
        static FileStream Relatorio_Aluno;
        static StreamWriter Esc_Rel_Aluno;
        static FileStream Relatorio_SI;
        static StreamWriter Esc_Rel_SI;

        static void Inicializar()
        {
            Arquivo = new FileStream(NomeArq, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Escreve_Arquivo = new BinaryWriter(Arquivo);
            Ler_Arquivo = new BinaryReader(Arquivo);
            Auxiliar = new FileStream(NomeAux, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Aux = new BinaryWriter(Auxiliar);
            Ler_Aux = new BinaryReader(Auxiliar);
            Substituta = new FileStream("subs.dat", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Subs = new BinaryWriter(Substituta);
            Subs_ler = new BinaryReader(Substituta);
            Inome = new FileStream(inm, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            idxNome = new BinaryWriter(Inome);
            Ler_idxNome = new BinaryReader(Inome);
            IMat = new FileStream(imt, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            idxMat = new BinaryWriter(IMat);
            Ler_idxMat = new BinaryReader(IMat);
            Relatorio = new FileStream(relatorio, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Escreve_Relatorio = new StreamWriter(Relatorio);
            Relatorio_Aluno = new FileStream(Aluno, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Esc_Rel_Aluno = new StreamWriter(Relatorio_Aluno);
            Relatorio_SI = new FileStream(rel_SI, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            Esc_Rel_SI = new StreamWriter(Relatorio_SI);
        }

        static void Finalizar()
        {
            Escreve_Arquivo.Close();
            Ler_Arquivo.Close();
            Arquivo.Close();
            Aux.Close();
            Ler_Aux.Close();
            Auxiliar.Close();
            Subs.Close();
            Subs_ler.Close();
            Substituta.Close();
            idxMat.Close();
            Ler_idxMat.Close();
            IMat.Close();
            idxNome.Close();
            Ler_idxNome.Close();
            Inome.Close();
            Escreve_Relatorio.Close();
            Relatorio.Close();
            Esc_Rel_Aluno.Close();
            Relatorio_Aluno.Close();
            Esc_Rel_SI.Close();
            Relatorio_SI.Close();
        }
        static void Main(string[] args)
        {
            Inicializar();
            Menu();
            Finalizar();
        }
        static void Menu()
        {
            byte opcao;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("SISTEMA DE CADASTRO DE ALUNOS");
                Console.WriteLine("=============================\n\n");
                Console.ResetColor();
                Console.WriteLine("1 - Cadastrar Aluno");
                Console.WriteLine("2 - Excluir Aluno");
                Console.WriteLine("3 - Consultar Aluno por Nome");
                Console.WriteLine("4 - Consultar Aluno por Matricula");
                Console.WriteLine("5 - Relatório de alunos da universidade");
                Console.WriteLine("6 - Relatório de alunos por curso");
                Console.WriteLine("7 - Relatório de alunos por curso de SI");
                Console.WriteLine("8 - Gerar Indíce");
                Console.WriteLine("9 - Quantidade de alunos matriculados");
                Console.WriteLine("10 - Sair\n");
                Console.Write("Digite sua opção : ");
                opcao = byte.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Cadastrar_Aluno();
                        break;
                    case 2:
                        Excluir_Aluno();
                        break;
                    case 3:
                        Consultar_Aluno_Nome();
                        break;
                    case 4:
                        Consultar_Aluno_Matricula();
                        break;
                    case 5:
                        Relatório_Universidade();
                        break;
                    case 6:
                        Relatório_Curso();
                        break;
                    case 7:
                        Relatório_Curso_SI();
                        break;
                    case 8:
                        GerarIndices();
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("\n\n\nÍndice gerado com sucesso!");
                        Console.Write("\nPressione uma tecla para continuar...");
                        Console.ResetColor();
                        Console.ReadKey();
                        break;
                    case 9:
                        consulta();
                        break;
                    case 10:
                        opcao = 10;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;



                }
            } while (opcao >= 1 && opcao <= 9);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para sair...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static public void Cadastrar_Aluno()
        {
            Arquivo.Seek(Arquivo.Length, SeekOrigin.Begin);
            Aluno Al = new Aluno();
            Console.Clear();
            bool achou = true;
            while(achou)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("CADASTRO DE ALUNOS");
                Console.WriteLine("==================\n\n");
                Console.ResetColor();
                Console.Write("CURSOS DISPONÍVEIS\n\n");
                Console.Write("Sistemas de Informação(SI)\nNutrição(NT)\nAdministração(ADM)");
                string curso;
                Console.Write("\n\nCurso.....: ");
                curso = Console.ReadLine();
                if (curso == "SI" || curso == "Sistemas de Informação" || curso == "Nutrição" || curso == "NT" || curso == "ADM" || curso == "Administração")
                {
                    Al.Curso = curso;
                    achou = false;
                }
                else
                {
                    Console.WriteLine("\n\n\nNome de curso incorreto ou curso não disponível.");
                    Console.WriteLine("Informe o curso conforme nome ou sigla do menu.");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write("\n\n\nTecla enter para continuar...");
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }

            }
            Console.Write("Matrícula.: ");
            Al.Matricula = short.Parse(Console.ReadLine());
            Console.Write("Nome......: ");
            Al.Nome = Console.ReadLine();
            Console.Write("Período...: ");
            Al.Periodo = short.Parse(Console.ReadLine());
            Console.Write("Rua.......: ");
            Al.Rua = Console.ReadLine();
            Console.Write("Bairro....: ");
            Al.Bairro = Console.ReadLine();
            Console.Write("Estado....: ");
            Al.Estado = Console.ReadLine();
            Console.Write("Data_de_Nascimento\n");
            Console.Write("Dia.......: ");
            Al.Dia = short.Parse(Console.ReadLine());
            Console.Write("Mes.......: ");
            Al.Mes = short.Parse(Console.ReadLine());
            Console.Write("Ano.......: ");
            Al.Ano = int.Parse(Console.ReadLine());
            Console.Write("Telefone..: ");
            Al.Telefone = long.Parse(Console.ReadLine());
            Al.Salvar(Escreve_Arquivo);
            int contador = 1;
            bool gravou = false;
            Auxiliar.Seek(0, SeekOrigin.Begin);
            if (!(Ler_Arquivo.PeekChar() >= 0) && Ler_Aux.PeekChar() < 0)
            {
                Subs.Write(contador);
                Subs.Write("--");
                Subs.Write("--");
                gravou = true;
            }
            Auxiliar.Seek(0, SeekOrigin.Begin);
            if (!gravou)
            {
                Subs.Write(Ler_Aux.ReadInt32() + 1);
                Subs.Write(Ler_Aux.ReadString());
                Subs.Write(Ler_Aux.ReadString());
                gravou = true;
            }
            if (gravou)

            {
                Finalizar();
                File.Delete(NomeAux);
                File.Move("subs.dat", NomeAux);
                File.Delete("subs.dat");
                Inicializar();
            }
            Auxiliar.Seek(0, SeekOrigin.Begin);
            //Gera índice a cada tres matriculas
            if (Ler_Aux.ReadInt32() % 3 == 0)
                GerarIndices();
        }

        static void Excluir_Aluno()
        {

            BinaryWriter temp = new BinaryWriter(new FileStream("temp.dat", FileMode.Create, FileAccess.Write));
            string nome = "A";
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("EXCLUSÃO DE ALUNOS (POR MATRÍCULA)");
            Console.WriteLine("==================================\n\n");
            Console.ResetColor();
            Arquivo.Seek(0, SeekOrigin.Begin);
            Auxiliar.Seek(0, SeekOrigin.Begin);
            short Matricula, matr = 0;
            bool achou = false;
            Aluno Al = new Aluno();
            Console.Write("Matrícula : ");
            Matricula = short.Parse(Console.ReadLine());
            while (Ler_Arquivo.PeekChar() >= 0)
            {
                Al.Ler(Ler_Arquivo);
                if (Al.Matricula != Matricula)
                {
                    Al.Salvar(temp);
                }
                else
                {
                    achou = true;
                    nome = Al.Nome;
                    matr = Al.Matricula;
                }
            }
            temp.Close();
            
            if (achou)
            {
                Subs.Write(Ler_Aux.ReadInt32() - 1);
                Subs.Write(Ler_Aux.ReadString());
                Subs.Write(Ler_Aux.ReadString());
                Finalizar();
                File.Delete(NomeArq);
                File.Move("temp.dat", NomeArq);
                Console.WriteLine("Aluno excluído com sucesso.\n");
                File.Delete(NomeAux);
                File.Move("subs.dat", NomeAux);
                File.Delete("subs.dat");
                File.Delete("temp.dat");
                Inicializar();
            }
            else
            {
                File.Delete("temp.dat");
                Console.WriteLine("Aluno não localizado.\n");
            }
            Excluir_Indice_nome(nome);
            Excluir_Indice_mat(matr);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Pressione qualquer tecla...");
            Console.ResetColor();
            Console.ReadKey();


        }
        static void Excluir_Indice_nome(string nome)
        {
            BinaryWriter temp = new BinaryWriter(new FileStream("temp.dat", FileMode.Create, FileAccess.Write));
            Inome.Seek(0, SeekOrigin.Begin);
            bool achou = false;
            string compara;
            long posicao;
            //Exclui do índice de nome
            while (Ler_idxNome.PeekChar() >= 0)
            {
                compara = Ler_idxNome.ReadString();
                posicao = Ler_idxNome.ReadInt64();
                if (nome != compara)
                {
                    temp.Write(compara);
                    temp.Write(posicao);
                }
                else
                    achou = true;
            }
            temp.Close();

            if (achou)
            {
                Finalizar();
                File.Delete(inm);
                File.Move("temp.dat", inm);
                File.Delete("temp.dat");
                Inicializar();
            }
            else
            {
                File.Delete("temp.dat");
            }

        }
        static void Excluir_Indice_mat(short matr)
        {
            BinaryWriter temp = new BinaryWriter(new FileStream("temp.dat", FileMode.Create, FileAccess.Write));
            IMat.Seek(0, SeekOrigin.Begin);
            short comp_mat;
            long comp_pos;
            bool achou = false;
            //Exclui do índice de matrícula
            while (Ler_idxMat.PeekChar() >= 0)
            {
                comp_mat = Ler_idxMat.ReadInt16();
                comp_pos = Ler_idxMat.ReadInt64();
                if (matr != comp_mat)
                {
                    temp.Write(comp_mat);
                    temp.Write(comp_pos);
                }
                else
                    achou = true;
            }
            temp.Close();

            if (achou)
            {
                Finalizar();
                File.Delete(imt);
                File.Move("temp.dat", imt);
                File.Delete("temp.dat");
                Inicializar();
            }
            else
            {
                File.Delete("temp.dat");
            }
        }

        static void Consultar_Aluno_Nome()
        {
            Inome.Seek(0, SeekOrigin.Begin);
            string nome, confere_nome;
            long posicao;
            bool achou = false;
            Aluno Al = new Aluno();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("CONSULTA DE ALUNO POR NOME");
            Console.WriteLine("==========================\n\n");
            Console.ResetColor();
            Console.Write("Informe o nome do aluno: ");
            nome = Console.ReadLine();
            while (Ler_idxNome.PeekChar() >= 0)
            {
                confere_nome = Ler_idxNome.ReadString();
                posicao = Ler_idxNome.ReadInt64();
                if (confere_nome == nome)
                {
                    Arquivo.Seek(posicao, SeekOrigin.Begin);
                    Al.Ler(Ler_Arquivo);
                    Console.Write("\nMatrícula: {0}", Al.Matricula);
                    Console.Write("\nCurso....: {0}\nPeríodo...: {1}", Al.Curso, Al.Periodo);
                    Console.Write("\nNome.....: {0}\nRua.......: {1}", Al.Nome, Al.Rua);
                    Console.Write("\nBairro...: {0}\nEstado....: {1}", Al.Bairro, Al.Estado);
                    Console.Write("\nData_Nasc: {0}/{1}/{2}", Al.Dia, Al.Mes, Al.Ano);
                    Console.Write("\nTelefone.: {0}", Al.Telefone);
                    achou = true;
                }


            }
            if (!achou)
                Console.Write("\n\nAluno não localizado.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void Consultar_Aluno_Matricula()
        {
            IMat.Seek(0, SeekOrigin.Begin);
            short Matricula, Confere_Matricula;
            long posicao;
            bool achou = false;
            Aluno Al = new Aluno();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("CONSULTA DE ALUNO POR MATRÍCULA");
            Console.WriteLine("===============================\n\n");
            Console.ResetColor();
            Console.Write("INFORME A MATRICULA :  ");
            Matricula = short.Parse(Console.ReadLine());
            while (Ler_idxMat.PeekChar() >= 0 && achou == false)
            {
                Confere_Matricula = Ler_idxMat.ReadInt16();
                posicao = Ler_idxMat.ReadInt64();
                if (Confere_Matricula == Matricula)
                {
                    Arquivo.Seek(posicao, SeekOrigin.Begin);
                    Al.Ler(Ler_Arquivo);
                    Console.WriteLine("\n\nCurso....: " + Al.Curso);
                    Console.WriteLine("Período..: " + Al.Periodo);
                    Console.WriteLine("Nome.....: " + Al.Nome);
                    Console.WriteLine("Rua......: " + Al.Rua);
                    Console.WriteLine("Bairro...: " + Al.Bairro);
                    Console.WriteLine("Estado...: " + Al.Estado);
                    Console.WriteLine("Data_Nasc: " + Al.Dia + "/" + Al.Mes + "/" + Al.Ano);
                    Console.Write("Telefone.: " + Al.Telefone);

                    achou = true;
                }
            }
            if (!achou)
                Console.Write("\n\nAluno não localizado.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void Relatório_Universidade()
        {
            Finalizar();
            File.Delete(Aluno);
            Inicializar();
            Inome.Seek(0, SeekOrigin.Begin);
            int opcao = 1, retorno = 0;
            while (retorno == 0)
            {
                Auxiliar.Seek(4, SeekOrigin.Begin);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("RELATÓRIO DE ALUNOS DA UNIVERSIDADE");
                Console.WriteLine("===================================\n\n");
                Console.ResetColor();
                Console.Write("Última atualização de índice: {0} as {1}\n\n", Ler_Aux.ReadString(), Ler_Aux.ReadString());
                Console.Write("Digite:\n\n1 - Atualizar o índice antes da pesquisa.\n2 - Realizar pesquisa sem a atualização.");
                Console.Write("\n\nDigite sua opção: ");
                opcao = int.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1: GerarIndices();
                        retorno = 1;
                        break;
                    case 2:
                        retorno = 1;
                        break;
                    default: Console.Clear();
                        Console.Write("OPÇÃO INVÁLIDA\n\n");
                        break;
                }
            }
            ArrayList ordena_nome = new ArrayList();
            while (Ler_idxNome.PeekChar() >= 0)
            {
                ordena_nome.Add(Ler_idxNome.ReadString());
                Ler_idxNome.ReadInt64();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("RELATÓRIO DE ALUNOS DA UNIVERSIDADE");
            Console.WriteLine("===================================\n\n");
            Console.ResetColor();
            Relatorio_Aluno.Seek(0, SeekOrigin.Begin);
            Esc_Rel_Aluno.WriteLine("RELATÓRIO DE ALUNOS DA UNIVERSIDADE");
            Esc_Rel_Aluno.Write("===================================\n");
            Esc_Rel_Aluno.WriteLine();
            Esc_Rel_Aluno.WriteLine();
            Esc_Rel_Aluno.WriteLine();
            for (int i = 0; i < ordena_nome.Count; i++)
            {
                Esc_Rel_Aluno.WriteLine(ordena_nome[i]);
                Console.WriteLine(ordena_nome[i]);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\nUm arquivo com a lista de alunos acima foi gerado com o nome Relatorio_Aluno.txt");
            Console.Write("\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void Relatório_Curso()
        {
            Finalizar();
            File.Delete(relatorio);
            Inicializar();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("RELATÓRIO DE ALUNOS POR CURSO");
            Console.WriteLine("=============================\n\n");
            Console.ResetColor();
            Console.Write("Um arquivo com as respectivas listas foi gerado com o nome Relatorio_Curso.txt");
            Hashtable guarda = new Hashtable();
            Aluno Al = new Aluno();
            Arquivo.Seek(0, SeekOrigin.Begin);
            while (Ler_Arquivo.PeekChar() >= 0)
            {
                Al.Ler(Ler_Arquivo);
                guarda.Add(Al.Nome, Al.Curso);
            }
            Arquivo.Seek(0, SeekOrigin.Begin);
            Escreve_Relatorio.WriteLine("ADMINISTRAÇÃO");
            Escreve_Relatorio.Write("======================");
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine();
            foreach (DictionaryEntry DE in guarda)
            {
                if ((string)DE.Value == "ADM" || (string)DE.Value == "Administração")
                    Escreve_Relatorio.WriteLine("- " + DE.Key);
            }
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine("SISTEMAS DE INFORMAÇÃO");
            Escreve_Relatorio.Write("======================");
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine();
            foreach (DictionaryEntry DE in guarda)
            {
                if ((string)DE.Value == "SI" || (string)DE.Value == "Sistemas de Informação")
                    Escreve_Relatorio.WriteLine("- " + DE.Key);
            }
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine("NUTRIÇÃO");
            Escreve_Relatorio.Write("======================");
            Escreve_Relatorio.WriteLine();
            Escreve_Relatorio.WriteLine();
            foreach (DictionaryEntry DE in guarda)
            {
                if ((string)DE.Value == "NT" || (string)DE.Value == "Nutrição")
                    Escreve_Relatorio.WriteLine("- " + DE.Key);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void consulta()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("QUANTIDADE DE ALUNOS MATRICULADOS");
            Console.WriteLine("=================================\n\n");
            Console.ResetColor();
            Auxiliar.Seek(0, SeekOrigin.Begin);
            Console.Write("{0} alunos matriculados", Ler_Aux.ReadInt32());
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();
        }
        static void Relatório_Curso_SI()
        {
            Finalizar();
            File.Delete(rel_SI);
            Inicializar();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("RELATÓRIO DE ALUNOS DE SISTEMAS DE INFORMAÇÃO");
            Console.WriteLine("=============================================\n\n");
            Console.ResetColor();
            Console.Write("Um arquivo com as respectivas listas foi gerado com o nome Relatorio_SI.txt");
            SortedDictionary<short, string> relatorio = new SortedDictionary<short, string>();
            Aluno Al = new Aluno();
            Arquivo.Seek(0, SeekOrigin.Begin);
            while (Ler_Arquivo.PeekChar() >= 0)
            {
                Al.Ler(Ler_Arquivo);
                if (Al.Curso == "SI" || Al.Curso == "Sistemas de Informação")
                    relatorio.Add(Al.Matricula, Al.Nome);
            }
            Esc_Rel_SI.WriteLine("RELATÓRIO DE ALUNOS DO CURSO DE SISTEMAS DE INFORMAÇÃO");
            Esc_Rel_SI.Write("======================================================");
            Esc_Rel_SI.WriteLine();
            Esc_Rel_SI.WriteLine();
            Esc_Rel_SI.WriteLine("MATRÍCULA\t\tNOME");
            Esc_Rel_SI.WriteLine();
            foreach (KeyValuePair<short, string> kvp in relatorio)
            {
                Esc_Rel_SI.WriteLine(kvp.Key + "\t\t\t" + kvp.Value);
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("\n\n\nPressione uma tecla para continuar...");
            Console.ResetColor();
            Console.ReadKey();

        }
        static void GerarIndices()
        {
            data_hora();
            SortedDictionary<short, long> dicMatricula = new SortedDictionary<short, long>();
            SortedDictionary<string, long> dicNome = new SortedDictionary<string, long>();
            Arquivo.Seek(0, SeekOrigin.Begin);
            Aluno Al = new Aluno();
            long ultimaPos = 0;
            while (Ler_Arquivo.PeekChar() >= 0)
            {
                ultimaPos = Arquivo.Position;
                Al.Ler(Ler_Arquivo);
                if (Al.Status != '*')
                {
                    dicMatricula.Add(Al.Matricula, ultimaPos);
                    dicNome.Add(Al.Nome, ultimaPos);
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
            Finalizar();
            Inicializar();
        }
        static void data_hora()
        {
            hora = DateTime.Now.ToShortTimeString();
            data = DateTime.Now.ToShortDateString();
            Auxiliar.Seek(0, SeekOrigin.Begin);
            Substituta.Seek(0, SeekOrigin.Begin);
            Subs.Write(Ler_Aux.ReadInt32());
            Subs.Write(data);
            Subs.Write(hora);
            Finalizar();
            File.Delete(NomeAux);
            File.Move("subs.dat", NomeAux);
            File.Delete("subs.dat");
            Inicializar();
           
        }

    }
}