namespace Yonetimsell.Shared.ResponseViewModels
{
    public class Response<T>
    {
        public T Data { get; set; }
        public string Error { get; set; }
        public bool IsSucceeded { get; set; }

        /// <summary>
        /// Bu metot, yapılan işlemin başarılı olduğu durumlarda başarılı bir cevapla birlikte üretilen datayı ve status code'u döndürmek için kullanılır.
        /// </summary>
        /// <param name="data">Geri döndürülecek veri</param>
        /// <returns>Response<typeparamref name="T"/></returns>
        public static Response<T> Success(T data)
        {
            return new Response<T>
            {
                Data = data,
                IsSucceeded = true
            };
        }


        /// <summary>
        /// Bu metot, işlemin başarılı olduğunu ifade eden bir status code döndürmek için kullanılır.
        /// </summary>
        /// <returns>Response<typeparamref name="T"/></returns>
        public static Response<T> Success()
        {
            return new Response<T>
            {
                Data = default(T),
                IsSucceeded = true
            };
        }


        /// <summary>
        /// Bu metot, geriye başarısız bir cevap olarak içinde bir hata olan hata listesini döndürmek için kullanılır.
        /// </summary>
        /// <param name="error">Hata metni</param>
        /// <returns>Response<typeparamref name="T"/></returns>
        public static Response<T> Fail(string error)
        {
            return new Response<T>
            {
                Error = error,
                IsSucceeded = false
            };
        }

    }
}
