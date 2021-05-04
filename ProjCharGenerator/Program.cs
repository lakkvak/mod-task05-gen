using System;
using System.Collections.Generic;
using System.IO;


namespace generator
{
   
    class GenBigram
    {
        private string syms = "абвгдежзийклмнопрстуфхцчшщьыэюя";
        private char[] data;
        private int size;
        private int sum;
        private int[,] partAmounts;
        private Random random = new Random();
        public GenBigram(int[,] freq)
        {
            size = syms.Length;
            data = syms.ToCharArray();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    sum += freq[i, j];
            partAmounts = new int[size, size];
            int s2 = 0;

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    s2 += freq[i, j];
                    partAmounts[i, j] = s2;
                }
        }
        
        private string getSym()
        {
            int r = random.Next(0, sum);
            int i, j = 0;

            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    if (r < partAmounts[i, j])
                        return data[i].ToString() + data[j].ToString();
                }
            }

            return data[i].ToString() + data[j].ToString();
        }
        public void Out(int steps, string name)
        {
            string output = "";
            for (int i = 0; i < steps; i++)
            {
                output += getSym();
                if (i != steps - 1)
                    output += ' ';
            }

            File.WriteAllText(name, output);
        }
    }
    class GenWord
    {
        private string[] data;
        private int size;
        private int sum;
        private int[] partAmounts;
        private Random random = new Random();
                        
        public GenWord(string[] words)
        {
            data = words;
            size = words.Length;
            partAmounts = new int[size];
            for (int i = 0; i < size; i++)
            {
                sum += i;
                partAmounts[i] = sum;
            }
        }
        private string getWord()
        {
            int r = random.Next(0, sum);
            int j;

            for (j = 0; j < size; j++)
            {
                if (r < partAmounts[j])
                    break;
            }
            return data[j];
        }
        public void Out(int steps, string name)
        {
            string output = "";
            for (int i = 0; i < steps; i++)
            {
                output += getWord();
                if (i != steps - 1)
                    output += ' ';
            }

            File.WriteAllText(name, output);
        }
    }
    class GenTwoWords
    {
        private string[] data;
        private int size;
        private int sum;
        private int[] partAmounts;
        private Random random = new Random();

        public GenTwoWords(string[] words)
        {
            data = words;
            size = words.Length;
            partAmounts = new int[size];
            for (int i = 0; i < size; i++)
            {
                sum += i;
                partAmounts[i] = sum;
            }
          
        }
        private string getWords()
        {
            int r = random.Next(0, sum);
            int j;

            for (j = 0; j < size; j++)
            {
                if (r < partAmounts[j])
                    break;
            }
            return data[j];
        }
        public void Out(int steps, string name)
        {
            string output = "";
            for (int i = 0; i < steps; i++)
            {
                output += getWords();
                if (i != steps - 1)
                    output += ' ';
            }

            File.WriteAllText(name, output);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            try
            {
                var strData = File.ReadAllLines("Bigram.txt");
                int[,] freq = new int[strData.Length, strData.Length];
                for (int i = 0; i < strData.Length; i++)               
                    for (int j = 0; j < strData.Length; j++)
                        freq[i, j] = Convert.ToInt32(strData[i].Split(' ').GetValue(j));

                

                var bigram = new GenBigram(freq);
                bigram.Out(1000, "bigramGenerated.txt");
                
                var strWord = File.ReadAllLines("OneWord.txt");
                var oneWord = new GenWord(strWord);
                oneWord.Out(1000,"oneWordGenerated.txt");

                var strTwoWords = File.ReadAllLines("TwoWords.txt");
                var twoWord = new GenTwoWords(strTwoWords);
                twoWord.Out(1000, "twoWordsGenerated.txt");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                
            }
            
        }
    }
}

