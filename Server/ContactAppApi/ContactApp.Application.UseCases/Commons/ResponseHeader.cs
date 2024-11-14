using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Reflection;

namespace ContactApp.Application.UseCases.Commons
{
    public class ResponseHeader
    {
        public static readonly Dictionary<int, string> StatusCodeDescriptions = InitializeStatusCodeDescriptions();

        private ObservableCollection<ResponseMessage> messages;

        public ICollection<ResponseMessage> Messages
        {
            get
            {
                return messages;
            }
            set
            {
                InitializeMessages(value);
            }
        }

        public PagingInfo PagingInfo { get; set; }

        public string Date { get; private set; } = DateTime.UtcNow.ToString("r");


        public string Status { get; private set; }

        public int StatusCode { get; private set; } = 200;


        public ResponseHeader()
        {
            InitializeMessages(Enumerable.Empty<ResponseMessage>());
        }

        public void SetDate(DateTime date)
        {
            Date = date.ToString("r");
        }

        private static Dictionary<int, string> InitializeStatusCodeDescriptions()
        {
            return (from prop in typeof(StatusCodes).GetFields(BindingFlags.Static | BindingFlags.Public)
                    group prop by (int)prop.GetValue(null)).ToDictionary((IGrouping<int, FieldInfo> group) => group.Key, (IGrouping<int, FieldInfo> group) => group.First().Name.Substring(9).SplitCamelCase());
        }

        private void InitializeMessages(IEnumerable<ResponseMessage> initialMessages)
        {
            messages = new ObservableCollection<ResponseMessage>(initialMessages);
            messages.CollectionChanged += delegate (object _, NotifyCollectionChangedEventArgs e)
            {
                if (e.OldItems != null && e.OldItems.Count > 0)
                {
                    RefreshStatusBasedOffMessages();
                }
                else if (e.NewItems != null)
                {
                    int num = (from ResponseMessage m in e.NewItems
                               select m.GetHttpStatusCode()).Max();
                    if (num > StatusCode)
                    {
                        StatusCode = num;
                        Status = StatusCodeDescriptions[StatusCode];
                    }
                }
            };
            RefreshStatusBasedOffMessages();
        }

        private void RefreshStatusBasedOffMessages()
        {
            if (Messages.Any())
            {
                StatusCode = Messages.Select((ResponseMessage m) => m.GetHttpStatusCode()).Max();
            }
            else
            {
                StatusCode = 200;
            }

            Status = StatusCodeDescriptions[StatusCode];
        }
    }
}
