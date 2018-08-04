using UnityEngine;
using System.Collections;

public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer,Camera camera)
	{
		//获得相机椎体的片
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		//渲染的片是否在主摄像机的片里面
		return GeometryUtility.TestPlanesAABB (planes, renderer.bounds);
	}
}
