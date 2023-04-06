using Microsoft.Extensions.ObjectPool;
using System.ComponentModel;

namespace MyFirstAPI.DataAccess
{
    public class MyDataCRUD
    {
        private string[] MyData;

        public void Create() 
        {
            MyData = new string[] { "zero", "one", "two", "three", "four", "five" };
        }
        public string Read(int index)
        {
            return MyData[index];
        }
        public string[] Read()
        {
            return MyData;
        }
        public void Update(int index, string value) 
        {
            MyData[index] = value;

        }
        public void Delete(int index)
        {
            MyData[index] = "Deleted";
        }
    }
}
