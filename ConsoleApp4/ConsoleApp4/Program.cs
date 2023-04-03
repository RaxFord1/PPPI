using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

class MachineLearningData
{
    private int batchSize;
    public string[] data;
    protected double[] weights;
    internal bool shuffleData;
    protected internal string modelFile;

    public string TrainModel()
    {
        Console.WriteLine("Training machine learning model...");
        return "Training error: 0.2";
    }

    private void PreprocessData(int num)
    {
        Console.WriteLine($"Preprocessing {num} data points...");
    }

    protected internal void EvaluateModel(string dataFilePath)
    {
        Console.WriteLine($"Evaluating machine learning model using {dataFilePath}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var mlData = new MachineLearningData();
        mlData.modelFile = "model.json";

        // Using Type and TypeInfo
        Type type = typeof(MachineLearningData);
        TypeInfo typeInfo = type.GetTypeInfo();

        Console.WriteLine($"Type name: {type.Name}");
        Console.WriteLine($"Type namespace: {type.Namespace}");
        Console.WriteLine($"Type is abstract: {type.IsAbstract}");
        Console.WriteLine($"Type is sealed: {type.IsSealed}");

        Console.WriteLine($"Type info is abstract: {typeInfo.IsAbstract}");
        Console.WriteLine($"Type info is sealed: {typeInfo.IsSealed}");
        Console.WriteLine($"Type info has default constructor: {typeInfo.DeclaredConstructors.Any(c => c.IsPublic && c.GetParameters().Length == 0)}");

        // Using MemberInfo
        MemberInfo[] members = type.GetMembers();
        foreach (MemberInfo member in members)
        {
            Console.WriteLine($"Member name: {member.Name}, Member type: {member.MemberType}");
        }

        // Using FieldInfo
        string[] fields = new string[] { "data", "batchSize", "weights", "shuffleData", "modelFile" };
        foreach (string fieldname in fields) {
            FieldInfo field = type.GetField(fieldname);
            if (field != null)
            {
                Console.WriteLine($"Field name: {field.Name}, Field type: {field.FieldType}");
            }
            else
            {
                Console.WriteLine($"Field {fieldname} is null");
            }
        }

        // Using MethodInfo
        Console.WriteLine($"Trying to invoke : protected internal void EvaluateModel");
        MethodInfo method = type.GetMethod("EvaluateModel");
        if (method != null)
        {
            object result = method.Invoke(mlData, new object[] { "data.csv" });
            Console.WriteLine($"EvaluateModel result: {result}");
        }
        else {
            Console.WriteLine($"EvaluateModel is null");
        }

        Console.WriteLine($"Trying to invoke : private void PreprocessData");
        method = type.GetMethod("PreprocessData");
        if (method != null)
        {
            object result = method.Invoke(mlData, new object[] { });
            Console.WriteLine($"PreprocessData result: {result}");
        }
        else
        {
            Console.WriteLine($"PreprocessData is null");
        }

        Console.WriteLine($"Trying to invoke : public string TrainModel");
        method = type.GetMethod("TrainModel");
        if (method != null)
        {
            object result = method.Invoke(mlData, new object[] {});
            Console.WriteLine($"TrainModel result: {result}");
        }
        else
        {
            Console.WriteLine($"TrainModel is null");
        }
    }
}