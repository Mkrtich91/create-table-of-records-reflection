using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TableOfRecords
{
    /// <summary>
    /// Presents method that write in table form to the text stream a set of elements of type T.
    /// </summary>
    public static class TableOfRecordsCreator
    {
        /// <summary>
        /// Write in table form to the text stream a set of elements of type T (<see cref="ICollection{T}"/>),
        /// where the state of each object of type T is described by public properties that have only build-in
        /// type (int, char, string etc.)
        /// </summary>
        /// <typeparam name="T">Type selector.</typeparam>
        /// <param name="collection">Collection of elements of type T.</param>
        /// <param name="writer">Text stream.</param>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="collection"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Throw if <paramref name="writer"/> is null.</exception>
        /// <exception cref="ArgumentException">Throw if <paramref name="collection"/> is empty.</exception>
        public static void WriteTable<T>(ICollection<T>? collection, TextWriter? writer)
        {
            ArgumentNullException.ThrowIfNull(collection);
            ArgumentNullException.ThrowIfNull(writer);

            if (collection.Count < 1)
            {
                throw new ArgumentException("Collection is empty", nameof(collection));
            }

            Type type = typeof(T);
            PropertyInfo[] propertyInfo = type.GetProperties();

            List<int> columnWidth = propertyInfo.Select(property => property.Name.Length).ToList();

            foreach (var item in collection)
            {
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    string propertyValue = propertyInfo[i].GetValue(item)?.ToString() ?? string.Empty;
                    columnWidth[i] = Math.Max(columnWidth[i], propertyValue.Length);
                }
            }

            string boundary = "+" + string.Join("+", columnWidth.Select(width => new string('-', width + 2))) + "+";

            StringBuilder header = new StringBuilder("|");

            for (int i = 0; i < propertyInfo.Length; i++)
            {
                header.Append(" " + propertyInfo[i].Name.PadRight(columnWidth[i]) + " |");
            }

            writer.WriteLine(boundary);
            writer.WriteLine(header.ToString());
            writer.WriteLine(boundary);

            foreach (var item in collection)
            {
                var rowValues = propertyInfo.Select(
                    (property, index) =>
                    {
                        string propertyValue = property.GetValue(item)?.ToString() ?? string.Empty;

                        if (property.PropertyType == typeof(string) || property.PropertyType == typeof(char))
                        {
                            return propertyValue.PadRight(columnWidth[index]) + " ";
                        }

                        return propertyValue.PadLeft(columnWidth[index]) + " ";
                    }).ToArray();

                writer.WriteLine("| " + string.Join("| ", rowValues) + "|");
                writer.WriteLine(boundary);
            }
        }
    }
}
