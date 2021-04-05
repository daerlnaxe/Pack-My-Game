using Pack_My_Game.Cont;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pack_My_Game.Files
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
                if (!zeFold.Children.ContainsKey(pathName))
                {
                    zeFold.Children.Add(pathName, newFold);
                }
                MakeListFolder(newFold);


            }
        }


        /// <summary>
        /// Write structure of folder
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="root"></param>
        internal static void MakeStruct(Folder tree, string root)
        {
            string path = Path.Combine(root, "Struct.info");
            using (StreamWriter fs = new StreamWriter(path, false))
            {
                fs.WriteLine(FoncSchem.TraitListFolder(tree, 0,
                            Common.Games,
                            Common.CheatCodes,
                            Common.Images,
                            Common.Manuals,
                            Common.Musics,
                            Common.Videos));
            }
        }

        /// <summary>
        /// Trait a Folder 
        /// </summary>
        /// <param name="zeFold"></param>
        /// <param name="lvl"></param>
        /// <returns></returns>
        public static string TraitListFolder(Folder zeFold, int lvl, params string[] specialFolders)
        {
            string ret = null;

            string[] files = Directory.GetFiles(zeFold.Path, "*.*", SearchOption.TopDirectoryOnly);


            if (!specialFolders.Contains(zeFold.Name))
            {
                ret = $"{new string('\t', lvl)}╠ {zeFold.Name}: {files.Length} files\n";

            }
            else
            {
                string[] subFiles = Directory.GetFiles(zeFold.Path, "*.*", SearchOption.AllDirectories);
                ret = $"{new string('\t', lvl)}╠ {zeFold.Name}: {files.Length}/{subFiles.Length} files\n";
            }

            //Console.WriteLine( $"{new string('\t', lvl)}╠ {zeFold.Name}: {files.Length} files");

            foreach (string file in files)
            {
                //Console.WriteLine($"{new string('\t',lvl)}|\t {Path.GetFileName(file)}");
                ret += $"{new string('\t', lvl)}|\t {Path.GetFileName(file)}\n";
            }


            foreach (var child in zeFold.Children)
            {
                ret += TraitListFolder(child.Value, lvl + 1, specialFolders);
            }
            return ret;
        }
    }
}
