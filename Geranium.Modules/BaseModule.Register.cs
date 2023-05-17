using System;

namespace Geranium.Modules
{
	public abstract partial class BaseModule
	{
		/// <summary>
		/// Register component in module
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="isDefault"></param>
		public void Register<TComponent, TImplementation>(bool isDefault = false) => Services.RegisterTransient<TComponent, TImplementation>(isDefault);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="for"></param>
		public void Register(Type comp, params Type[] @for) => Services.RegisterTransient(comp, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="isDefault"></param>
		/// <param name="for"></param>
		public void Register(Type comp, bool isDefault = false, params Type[] @for) => Services.RegisterTransient(comp, isDefault, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="isDefault"></param>
		public void RegisterTransient<TComponent, TImplementation>(bool isDefault = false) => Services.RegisterTransient<TComponent, TImplementation>(isDefault);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="for"></param>
		public void RegisterTransient(Type comp, params Type[] @for) => Services.RegisterTransient(comp, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="isDefault"></param>
		/// <param name="for"></param>
		public void RegisterTransient(Type comp, bool isDefault = false, params Type[] @for) => Services.RegisterTransient(comp, isDefault, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="isDefault"></param>
		public void RegisterSingleton<TComponent, TImplementation>(bool isDefault = false) => Services.RegisterSingleton<TComponent, TImplementation>(isDefault);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="for"></param>
		public void RegisterSingleton(Type comp, params Type[] @for) => Services.RegisterSingleton(comp, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="isDefault"></param>
		/// <param name="for"></param>
		public void RegisterSingleton(Type comp, bool isDefault = false, params Type[] @for) => Services.RegisterSingleton(comp, isDefault, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <typeparam name="TComponent"></typeparam>
		/// <typeparam name="TImplementation"></typeparam>
		/// <param name="isDefault"></param>
		public void RegisterScoped<TComponent, TImplementation>(bool isDefault = false) => Services.RegisterScoped<TComponent, TImplementation>(isDefault);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="for"></param>
		public void RegisterScoped(Type comp, params Type[] @for) => Services.RegisterScoped(comp, @for);

		/// <summary>
		/// Register component in module
		/// </summary>
		/// <param name="comp"></param>
		/// <param name="isDefault"></param>
		/// <param name="for"></param>
		public void RegisterScoped(Type comp, bool isDefault = false, params Type[] @for) => Services.RegisterScoped(comp, isDefault, @for);
	}
}