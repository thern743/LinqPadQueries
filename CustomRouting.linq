<Query Kind="Program" />

void Main()
{
	var test = new TonyLambda();	
	var request = new Request { Resource = "/one/two/{value}", Path = "/hello/world/there", PathParameters = new Dictionary<string, string> { { "value", "one"}} };
	
	test.Handler(request);

	request = new Request { Resource = "/three/four/{value}", Path = "/hello/world/there", PathParameters = new Dictionary<string, string> { { "value", "two" } } };
	test.Handler(request);

	request = new Request { Resource = "/five/six/{value}", Path = "/hello/world/there", PathParameters = new Dictionary<string, string> { { "value", "three" } } };
	test.Handler(request);
}

public class Request
{
	public string Resource { get; set; }
	public string Body { get; set; }
	public string Path { get; set; }
	public IDictionary<string, string> PathParameters { get; set; }
}

public class Response
{
	public string body { get; set; }
	public int statusCode { get; set; }
	public bool isBase64Encoded { get; set; }
}

public class LambdaRouteAggregate
{
	public string MethodName { get; set; }	
	public MethodInfo MethodInfo { get; set; }
	public LambdaRouteAttribute RouteAttribute { get; set; }
}

public class BaseLambda 
{
	public virtual Response Handler(Request request)
	{
		var controllerFactory = new ControllerFactory<LambdaRouteAttribute, TonyController>();
		var aggregate = controllerFactory.GetRouteAggregate(request.Resource);
		var controllerInstance = controllerFactory.GetInstance();
		var invoker = new ControllerMethodInvoker<Response>();
		var response = invoker.Invoke(aggregate, controllerInstance, new object[] { request });
		return response;
	}
}

public class TonyLambda : BaseLambda
{
	public override Response Handler(Request request)
	{
		Console.WriteLine($"Request Recevied. Routing: {request.Resource}");
		var response = base.Handler(request);
		Console.WriteLine($"Result = {response?.body}");
		return response;
	}
}

public class ControllerFactory<TAttribute, TController> where TAttribute : LambdaRouteAttribute where TController : LambdaApiController
{
	public LambdaRouteAggregate GetRouteAggregate(string resource)
	{
		var aggregate = typeof(TController)
						.GetMethods()
						.Select(x => new LambdaRouteAggregate { RouteAttribute = (TAttribute)Attribute.GetCustomAttribute(x, typeof(TAttribute)), MethodName = x.Name, MethodInfo = x })
						.FirstOrDefault(x => x.RouteAttribute != null && x.RouteAttribute.Resource.Equals(resource));
		return aggregate;
	}

	public TController GetInstance()
	{
		var controllerType = typeof(TController);
		var controllerInstance = Activator.CreateInstance(controllerType);
		return (TController)controllerInstance;
	}
}

public class ControllerMethodInvoker<T> 
{
	public T Invoke(LambdaRouteAggregate aggregate, object controllerInstance, object[] data )
	{
		var response = (T)aggregate?.MethodInfo.Invoke(controllerInstance, data);
		return response;
	}
}

public class LambdaApiController 
{
	public LambdaApiController() 
	{
		Console.WriteLine("ApiController ctor!");
	}
}

public class TonyController : LambdaApiController
{
	[LambdaRoute("/one/two/{value}")]
	public Response TestRouteOne(Request request)
	{
		Console.WriteLine("Executing TestRouteOne");
		return new Response() { body = "TestRouteOne" };
	}

	[LambdaRoute("/three/four/{value}")]
	public Response TestRouteTwo(Request request)
	{
		Console.WriteLine("Executing TestRouteTwo");
		return new Response() { body = "TestRouteOne" };
	}
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public class LambdaRouteAttribute : Attribute
{
	public string Resource { get; private set; }	

	public LambdaRouteAttribute(string resource)
	{
		if (string.IsNullOrWhiteSpace(resource)) throw new ArgumentNullException("resource");
		Resource = resource;
	}	
}