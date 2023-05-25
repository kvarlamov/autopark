namespace Generator.UseCases.Abstract
{
    internal interface IUseCase<TOption>
    {
        void Execute(TOption option);
    }
}