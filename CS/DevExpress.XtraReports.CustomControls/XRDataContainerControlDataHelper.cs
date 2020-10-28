using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DevExpress.XtraReports.CustomControls
{
    public class XRDataContainerControlDataHelper
    {
        XRDataContainerControl control;

        public XRDataContainerControlDataHelper(XRDataContainerControl control)
        {
            this.control = control;
        }

        protected static PropertyDescriptor GetDescriptorByFieldName(PropertyDescriptorCollection fields, string name)
        {
            if (name.Trim() != string.Empty)
                for (int i = 0; i < fields.Count; i++)
                    if (fields[i].DisplayName == name)
                        return fields[i];
            return null;
        }

        protected virtual void InitializeRecord(XRDataRecord record, object data) { }

        protected internal virtual void LoadData()
        {
            control.Records.Clear();
            LoadRecords();
        }

        private void LoadRecords() 
        {
            IList list = ((ICustomDataContainer)control).GetDataSource();
            PropertyDescriptorCollection fields = control.GetAvailableFields();

            List<PropertyDescriptor> visibleFields = new List<PropertyDescriptor>();

            for (int i = 0; i < control.Headers.Count; i++)
            {
                PropertyDescriptor descriptor = GetDescriptorByFieldName(fields, control.Headers[i].FieldName);
                visibleFields.Add(descriptor);
            }            

            if (list == null)
                return;

            foreach (object dataItem in list)
            {
                XRDataRecord record = control.CreateDataRecord();
                control.Records.Add(record);

                for (int i = 0; i < control.Headers.Count; i++)
                    if (visibleFields[i] != null)
                        record[i] = visibleFields[i].GetValue(dataItem);
                    else
                        record[i] = null;

                InitializeRecord(record, dataItem);
            }
        }

        protected internal virtual void SortData()
        {
            control.Records.Sort();
        }
    }
}
