using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using WPF_SHF_Element_lib.Models;

namespace WPF_SHF_Element_lib
{
    public class Element
    {
        public string imagePath { get; set; }
        public int group { get; set; }
        public string name { get; set; }
        public List<DataGrid1_Parameters> parameters { get; set; }
        public List<DataGrid1_Elements> other_par { get; set; }
        public List<MatrixElements> matrix { get; set; }


        public void AddNewElement(string fileName, int group, string name, List<DataGrid1_Parameters> parameters, List<DataGrid1_Elements> values, List<MatrixElements> matrix,string imagePath)
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                AllowTrailingCommas = true,
                WriteIndented = true
            };
            string file_path = AppDomain.CurrentDomain.BaseDirectory + "elementsLib/" + fileName;
            if (!File.Exists(file_path))
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "elementsLib/");
                var elementsList = new List<Element>
                {
                    new Element()
                    {
                        group = group,
                        name = name,
                        parameters = parameters,
                        other_par = values,
                        matrix = matrix,
                        imagePath = imagePath
                        
                    }
                };
                File.WriteAllText(file_path, JsonSerializer.Serialize(elementsList, options));
            }
            else
            {
                var jsonData = File.ReadAllText(file_path);
                var elementsList = JsonSerializer.Deserialize<List<Element>>(jsonData) ?? new List<Element>();

                elementsList.Add(new Element()
                {
                    group = group,
                    name = name,
                    parameters = parameters,
                    other_par = values,
                    matrix = matrix,
                    imagePath = imagePath
                });
                jsonData = JsonSerializer.Serialize(elementsList, options);
                File.WriteAllText(file_path, jsonData);
            }
        }

        public static bool searchName()
        {
            string file_path = AppDomain.CurrentDomain.BaseDirectory + Data.fileName;
            if (File.Exists(file_path))
            {
                var jsonData = File.ReadAllText(file_path);
                var elementsList = JsonSerializer.Deserialize<List<Element>>(jsonData) ?? new List<Element>();
                foreach (Element element in elementsList)
                {
                    if (element.name == Data.name) 
                    {
                        return true;
                        
                    }

                }
                return false;

            }
            else { return false; }
        }
    }
}
