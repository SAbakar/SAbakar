namespace Principal.Divers
{
    public class Response<T>
    {
        public Response()
        {}
        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        /*
        public Response(IEnumerable<T> dataList)
        {
            Succeeded = true;
            Message = dataList.Any()?$"Il y'a {dataList.Count()}  enregistrement(s)" :"Aucun enregistrement";
            Errors = null;
            DataList = dataList;
        }*/
        public T Data { get; set; }
        //public IEnumerable<T> DataList { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
