using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetImages_2
{
    public class Json
    {
        // Percorso del singolo file .json da importare di default
        private string _pathFileGetImagesConfig = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            @"\BOLD Software\GetImages\ConfigGetImagesPath.json";

        private List<JsonData> _data = new List<JsonData>();

        // Metodo per CREARE il file Json
        public List<JsonData> WriteJson(int id, string name, string path)
        {
            _data.Add(new JsonData()
            {
                Id = id,
                Name = name,
                Path = path
            });
            string jsonString = JsonConvert.SerializeObject(_data.ToArray());
            //write string to file
            File.WriteAllText(_pathFileGetImagesConfig, jsonString);

            return _data;
        }

        // Metodo per MODIFICARE o AGGIUNGERE un oggetto del file .json
        public void UpdateJson(int id, int index, string name, string path)
        {
            string jsonText = File.ReadAllText(_pathFileGetImagesConfig);
            IList<JsonData> traduction = JsonConvert.DeserializeObject<IList<JsonData>>(jsonText);
            dynamic jsonObj = JsonConvert.DeserializeObject(jsonText);
            // Se l'oggetto .json esiste già...
            if (traduction.Any(x => x.Id == id))
            {
                jsonObj[index]["Path"] = path;
                string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
                File.WriteAllText(_pathFileGetImagesConfig, output);
            }
            else // ... altrimenti lo crea
            {
                traduction.Add(new JsonData { Id = id, Name = name, Path = path });
                jsonText = JsonConvert.SerializeObject(traduction);
                File.WriteAllText(_pathFileGetImagesConfig, jsonText);
            }
        }
    }
}
