using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style.XmlAccess;
using RED.Models.ReportGeneration.EPPlus.ReportAttributes;

namespace RED.Models.ReportGeneration.EPPlus
{
    public abstract class EPPlusBaseGeneric<TReportModel> : EPPlusExcelBase
        where TReportModel : IReportable
    {
        public EPPlusBaseGeneric(ReportModel model)
            : base()
        {
            this.ReportModel = model;
        }

        public EPPlusBaseGeneric(ReportModel model, string fileName)
            : base(fileName)
        {
            this.ReportModel = model;
        }

        protected ReportModel ReportModel { get; set; }

        protected List<TReportModel> ReportData
        {
            get 
            {
                 return this.ReportModel.ReportItems as List<TReportModel>;
            }
        }

        protected ExcelRow PopulateLine(IReportable item, int row)
        {
            ExcelRow resultRow = InsertRow(row);
            this.PopulateLineGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow PopulateLine(IReportable item, int row, int copyStylesFromRow)
        {
            ExcelRow resultRow = InsertRow(row, copyStylesFromRow);
            PopulateLineGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow PopulateLine(IReportable item, int row, ExcelRow copyStylesFromRow)
        {
            ExcelRow resultRow = InsertRow(row, copyStylesFromRow);
            PopulateLineGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow PopulateLine(IReportable item, int row, string namedStyle)
        {
            ExcelRow resultRow = InsertRow(row, namedStyle);
            PopulateLineGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow PopulateLine(IReportable item, int row, ExcelNamedStyleXml namedStyle)
        {
            ExcelRow resultRow = InsertRow(row, namedStyle);
            PopulateLineGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow CreateTableHeader(IReportable item, int row)
        {
            ExcelRow resultRow = InsertRow(row);
            CreateHeaderGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow CreateTableHeader(IReportable item, int row, int copyStylesFromRow)
        {
            ExcelRow resultRow = InsertRow(row, copyStylesFromRow);
            CreateHeaderGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow CreateTableHeader(IReportable item, int row, ExcelRow copyStylesFromRow)
        {
            ExcelRow resultRow = InsertRow(row, copyStylesFromRow);
            CreateHeaderGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow CreateTableHeader(IReportable item, int row, string namedStyle)
        {
            ExcelRow resultRow = InsertRow(row, namedStyle);
            CreateHeaderGeneric(item, row);

            return resultRow;
        }

        protected ExcelRow CreateTableHeader(IReportable item, int row, ExcelNamedStyleXml namedStyle)
        {
            ExcelRow resultRow = InsertRow(row, namedStyle);
            CreateHeaderGeneric(item, row);

            return resultRow;
        }

        protected virtual void CreateTableFooter(IReportable item, int row)
        {
            throw new NotImplementedException("NOT IMPLEMENTED");
        }

        private void PopulateLineGeneric(IReportable item, int row)
        {
            IEnumerable<PropertyInfo> itemProperties = item.GetType().GetProperties()
                                                        .Where(p => p.IsDefined(typeof(ReportPropertyAttribute), false));

            //InsertRow(row); - THE INSERTION IS MADE BY THE OVERLOADS HOLDING IT, so it can specify styles if necessary
            foreach (var property in itemProperties)
            {
                ReportPropertyAttribute attr = (ReportPropertyAttribute)Attribute.GetCustomAttribute(
                    property, typeof(ReportPropertyAttribute));

                string address = string.Empty;
                string firstColumn = attr.Column;
                if (attr.Merged)
                {
                    //to merge cells if neccessary
                    string lastColumn = attr.LastColumn;
                    address = firstColumn + row + ":" + lastColumn + row;
                }
                else
                {
                    address = firstColumn + row;
                }

                Cells[address].Value = property.GetValue(item, null);
                Cells[address].Merge = attr.Merged;
            }
        }

        private void CreateHeaderGeneric(IReportable item, int row)
        {
            IEnumerable<PropertyInfo> itemProperties = item.GetType().GetProperties()
                                                        .Where(p => p.IsDefined(typeof(ReportHeaderPropertyAttribute), false));
            ////InsertRow(row); - THE INSERTION IS MADE BY THE OVERLOADS HOLDING IT, so it can specify styles if necessary
            foreach (var property in itemProperties)
            {
                var attr = (ReportHeaderPropertyAttribute)Attribute.GetCustomAttribute(property, typeof(ReportHeaderPropertyAttribute));

                string address = string.Empty;
                string firstColumn = attr.Column;
                if (attr.Merged)
                {
                    //to merge cells if neccessary
                    string lastColumn = attr.LastColumn;
                    address = firstColumn + row + ":" + lastColumn + row;
                }
                else
                {
                    address = firstColumn + row;
                }

                Cells[address].Value = attr.Value;
                Cells[address].Merge = attr.Merged;
            }
        }
    }
}