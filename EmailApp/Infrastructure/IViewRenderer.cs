namespace WebUi.Infrastructure
{
    public interface IViewRenderer
    {
        string Render<TModel>(string name, TModel model);
    }
}
