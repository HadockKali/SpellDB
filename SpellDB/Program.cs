using System;


namespace SpellDB
{
    static class MainClass
    {
        public static void Main(string[] args)
        {
            DDragonParser.ParseAndBuildSpellDb();

            Console.ReadKey();
        }
    }
}
