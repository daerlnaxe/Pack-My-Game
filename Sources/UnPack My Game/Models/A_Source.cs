﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using UnPack_My_Game.Cont;

namespace UnPack_My_Game.Models
{
    /// <summary>
    /// Give files
    /// </summary>
    public abstract class A_Source : A_Err
    {
        public virtual event BasicHandler Signal;

        public abstract List<FileObj> Games { get; set; } 

        public abstract void CheckErrors();

        public virtual void Add_Game(string linkGame)
        {
            if (string.IsNullOrEmpty(linkGame))
                return;
            //Games = new FileObj[0];
            /*else
            {*/
            Games.Add(new FileObj(linkGame));

            /*}*/

            Signal?.Invoke(this);
        }

        public virtual void Add_Game(FileObj game)
        {
            Games.Add(game);
            Signal?.Invoke(this);
        }

        public virtual void Remove_Game(FileObj game)
        {
            Games.Remove(game);
            Signal?.Invoke(this);
        }

        internal void RaiseError(string propertyName, string reason)
        {
            Remove_Error(propertyName);
            Add_Error(reason, propertyName);
        }
    }
}