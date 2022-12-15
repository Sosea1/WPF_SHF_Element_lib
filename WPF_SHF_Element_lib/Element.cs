﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace WPF_SHF_Element_lib
{
    public class Element
    {
        public string group { get; set; }
        public string name { get; set; }
        public string[] parameters { get; set; }
        public List<DataGrid1_Elements> other_par { get; set; }
        public string[] matrix { get; set; }


        public void AddNewElement(string group, string name, string[] parameters, List<DataGrid1_Elements> values, string[] matrix)
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                AllowTrailingCommas = true,
                WriteIndented = true
            };
            string file_path = @"C:\Users\user\source\repos\WPF_SHF_Element_lib\test.json";
            var jsonData = File.ReadAllText(file_path);
            var elementsList = JsonSerializer.Deserialize<List<Element>>(jsonData) ?? new List<Element>();

            elementsList.Add(new Element()
            {
                group = group,
                name = name,
                parameters = parameters,
                other_par = values,
                matrix = matrix
            }); ;

            jsonData = JsonSerializer.Serialize(elementsList, options);
            File.WriteAllText(file_path, jsonData);


        }
    }
}
