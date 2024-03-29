using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class AnagramFinder
{
    private Dictionary<string, List<string>> anagramDictionary;

    public void LoadDictionary(string filePath)
    {
        anagramDictionary = new Dictionary<string, List<string>>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string sortedWord = SortWord(line);
                    if (!anagramDictionary.ContainsKey(sortedWord))
                        anagramDictionary[sortedWord] = new List<string>();

                    anagramDictionary[sortedWord].Add(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors du chargement du dictionnaire : " + ex.Message);
        }
    }

    public List<string> FindAnagrams(string word)
    {
        string sortedWord = SortWord(word);
        if (anagramDictionary.ContainsKey(sortedWord))
            return anagramDictionary[sortedWord];

        return new List<string>();
    }

    private string SortWord(string word)
    {
        char[] characters = word.ToLower().ToCharArray();
        Array.Sort(characters);
        return new string(characters);
    }
}

class Program
{
    static void Main(string[] args)
    {
        string dictionaryFilePath = "dictionary.txt"; // Chemin vers le fichier dictionnaire

        AnagramFinder anagramFinder = new AnagramFinder();
        anagramFinder.LoadDictionary(dictionaryFilePath);

        Console.WriteLine("Entrez les lettres pour trouver les anagrammes (sans espace) :");
        string input = Console.ReadLine();

        List<string> anagrams = anagramFinder.FindAnagrams(input);

        Console.WriteLine("\nRésultats :");
        if (anagrams.Count > 0)
        {
            foreach (string anagram in anagrams)
            {
                Console.WriteLine(anagram);
            }
        }
        else
        {
            Console.WriteLine("Aucun anagramme trouvé ");
    }
}
