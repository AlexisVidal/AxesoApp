using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AxesoConsumer.Models
{
    public class CategoriaInfoRepository
    {
        private ObservableCollection<CategoriaInfo> categoriaInfo;

        public ObservableCollection<CategoriaInfo> CategoriaInfo
        {
            get { return categoriaInfo; }
            set { this.categoriaInfo = value; }
        }

        public CategoriaInfoRepository()
        {
            GenerateBookInfo();
        }

        internal void GenerateBookInfo()
        {
            categoriaInfo = new ObservableCollection<CategoriaInfo>();
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Object-Oriented Programming in C#", CategoriaDescription = "Object-oriented programming is a programming paradigm based on the concept of objects" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "C# Code Contracts", CategoriaDescription = "Code Contracts provide a way to convey code assumptions" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Machine Learning Using C#", CategoriaDescription = "You’ll learn several different approaches to applying machine learning" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Neural Networks Using C#", CategoriaDescription = "Neural networks are an exciting field of software development" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Visual Studio Code", CategoriaDescription = "It is a powerful tool for editing code and serves for end-to-end programming" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Android Programming", CategoriaDescription = "It is provides a useful overview of the Android application life cycle" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "iOS Succinctly", CategoriaDescription = "It is for developers looking to step into frightening world of iPhone" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Visual Studio 2015", CategoriaDescription = "The new version of the widely-used integrated development environment" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Xamarin.Forms", CategoriaDescription = "Its creates mappings from its C# classes and controls directly" });
            categoriaInfo.Add(new CategoriaInfo() { CategoriaName = "Windows Store Apps", CategoriaDescription = "Windows Store apps present a radical shift in Windows development" });
        }
    }
}