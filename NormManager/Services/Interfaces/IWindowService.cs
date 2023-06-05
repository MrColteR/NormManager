namespace NormManager.Services.Interfaces
{
    public interface IWindowService
    {
        /// <summary>
        /// Открыть окно по VM
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void Show<TViewModel>();

        /// <summary>
        /// Закрыть окно по VM
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        public void Close<TViewModel>();
    }
}
