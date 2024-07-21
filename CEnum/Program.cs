


public class CEnum<T>
{
	public readonly T Value;
	
	protected CEnum(T v){ Value = v; }
	
	public static implicit operator T(CEnum<T> This) => This.Value;//Deprecated : Reading "Value" directly is faster.
}

public class CEnum : CEnum<int>
{
	public const int Length = 0;
	
	public const int Default = 0; public static readonly CEnum EDefault = new(Default);
	
	protected CEnum(int v):base(v){}
}

public class EUser : CEnum
{
	public new const int Length = CEnum.Length + 2;
	
	public const int Hoge = 0; public static readonly EUser EHoge = new(Hoge);
	public const int Fuga = 1; public static readonly EUser EFuga = new(Fuga);
	
	protected EUser(int v):base(v){}
}

public class ETest : EUser
{
	public new const int Length = EUser.Length + 1;
	
	public const int Piyo = 2; public static readonly ETest EPiyo = new(Piyo);
	
	protected ETest(int v):base(v){}
}



namespace Program
{
	internal class Program
	{
		enum EEnum
		{
			Hoge,
			Fuga,
			Piyo,
		}
		
		static void Main(string[] args)
		{
			{	// 
				{	// 
					var v0 = EUser.EHoge;
					var v1 = EUser.EFuga;
					var v2 = ETest.EPiyo;
					Log($"{v0}.{v0.Value}");
					Log($"{v1}.{v1.Value}");
					Log($"{v2}.{v2.Value}");
					Log($"{v0}.{(int)v0}");
					Log($"{v1}.{(int)v1}");
					Log($"{v2}.{(int)v2}");
				}
				Log($"\n");
			}
			
			void Test<E>(System.Enum SE, E WE, CEnum CE) where E : System.Enum
			{
				int N = 10000000;
				
				for (int Retry = 3; Retry-- > 0;){
					Log($"--- value");
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						int c = 0;
						var e = EEnum.Piyo;
						for (int n = N; n > 0; --n) c += (int)e;
						Stopwatch.Stop();
						Log($"enum : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						int c = 0;
						var e = SE;
						for (int n = N; n > 0; --n) c += (int)(object)e;
						Stopwatch.Stop();
						Log($"Enum : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						int c = 0;
						var e = WE;
						for (int n = N; n > 0; --n) c += (int)(object)e;
						Stopwatch.Stop();
						Log($"where : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						int c = 0;
						var e = CE;
						for (int n = N; n > 0; --n) c += e.Value;
						Stopwatch.Stop();
						Log($"CEnum : {Stopwatch.Elapsed}");
					}
					
					Log($"--- switch");
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						var e = EEnum.Piyo;
						for (int n = N; n > 0; --n){
							switch (e){
								case EEnum.Hoge: break;
								case EEnum.Fuga: break;
								case EEnum.Piyo: break;
								default: break;
							}
						}
						Stopwatch.Stop();
						Log($"enum : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						var e = SE;
						for (int n = N; n > 0; --n){
							switch (e){
								case EEnum.Hoge: break;
								case EEnum.Fuga: break;
								case EEnum.Piyo: break;
								default: break;
							}
						}
						Stopwatch.Stop();
						Log($"Enum : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						var e = WE;
						for (int n = N; n > 0; --n){
							switch (e){
								case EEnum.Hoge: break;
								case EEnum.Fuga: break;
								case EEnum.Piyo: break;
								default: break;
							}
						}
						Stopwatch.Stop();
						Log($"where : {Stopwatch.Elapsed}");
					}
					
					{	// 
						var Stopwatch = new System.Diagnostics.Stopwatch();
						Stopwatch.Start();
						var e = CE.Value;
						for (int n = N; n > 0; --n){
							switch (e){
								case EUser.Hoge: break;
								case EUser.Fuga: break;
								case ETest.Piyo: break;
								default: break;
							}
						}
						Stopwatch.Stop();
						Log($"CEnum : {Stopwatch.Elapsed}");
					}
					
					Log($"\n");
				}
			};
			Test<EEnum>(EEnum.Piyo, EEnum.Piyo, ETest.EPiyo);
		}
		
		
		
		static void Log(string Text) => Console.WriteLine(Text);
	}
}
