using Pack_My_Game.Container;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Pack
{
    static class FoncSchem
    {
        //Folder zeMasterFold { get; set; }

        //public FoncSchem()
        //{

        //    //MakeList(0, @"E:\porno");
        //    string path = @"E:\porno";
        //    zeMasterFold = new Folder(Path.GetFileName(path), path);

        //    MakeListFolder(zeMasterFold);
        //    string ret =TraitListFolder(zeMasterFold,0);
        //    Console.WriteLine(ret);
        //}

        static void MakeList(int lvl, string path)
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);


            Console.WriteLine($"{new string('\t', lvl)} {path} : {files.Length} fichiers");

            foreach (var dir in Directory.EnumerateDirectories(path, "*.*", SearchOption.TopDirectoryOnly))
            {

                MakeList(lvl + 1, dir);
            }
        }

        public static void MakeListFolder(Folder zeFold)
        {

            string[] files = Directory.GetFiles(zeFold.Path, "*.*", SearchOption.TopDirectoryOnly);


            foreach (var dir in Directory.EnumerateDirectories(zeFold.Path, "*.*", SearchOption.TopDirectoryOnly))
            {
                string pathName = Path.GetFileName(dir);
                Folder newFold = new Folder(pathName, dir);
                if (!zeFold.Children.ContainsKey(pathName)) {
                    zeFold.Children.Add(pathName, newFold);
                }
                MakeListFolder(newFold);


            }
        }

        /// <summary>
        /// Trait a Folder 
        /// </summary>
        /// <param name="zeFold"></param>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public static string TraitListFolder(Folder zeFold, int lvl)
        {
            string ret = null;


            string[] files = Directory.GetFiles(zeFold.Path, "*.*", SearchOption.TopDirectoryOnly);
            //Console.WriteLine( $"{new string('\t', lvl)}╠ {zeFold.Name}: {files.Length} files");
            ret = $"{new string('\t', lvl)}╠ {zeFold.Name}: {files.Length} files\n";

            foreach (string file in files)
            {
                //Console.WriteLine($"{new string('\t',lvl)}|\t {Path.GetFileName(file)}");
                ret += $"{new string('\t', lvl)}|\t {Path.GetFileName(file)}\n";
            }


            foreach (var child in zeFold.Children)
            {
                ret += TraitListFolder(child.Value, lvl + 1);
            }
            return ret;
        }
    }
}
