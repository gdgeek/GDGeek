using UnityEngine;
using System.Collections;

public static class TransformUtility {

    public static void resetPos(this Transform transform)
    {
        transform.position = Vector3.zero;
    }

    public static void resetRot(this Transform transform)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
    }

    public static void resetScale(this Transform transform)
    {
        transform.localScale = Vector3.zero;
    }

    public static void resetAll(this Transform transform)
    {
        transform.resetPos();
        transform.resetRot();
        transform.resetScale();
    }

    public static void setX(this Transform transform, float x)  
	{  
		Vector3 newPosition =   
			new Vector3(x, transform.position.y, transform.position.z);  

		transform.position = newPosition;  
	}  

	public static void setY(this Transform transform, float y)  
	{  
		Vector3 newPosition =   
			new Vector3(transform.position.x, y, transform.position.z);  

		transform.position = newPosition;  
	}  

	public static void setZ(this Transform transform, float z)  
	{  
		Vector3 newPosition =   
			new Vector3(transform.position.x, transform.position.y, z);  

		transform.position = newPosition;  
	}  

	public static void plusX(this Transform transform, float x){
		Vector3 newPosition =   
			new Vector3(transform.position.x + x, transform.position.y, transform.position.z);  

		transform.position = newPosition;  
	}

	public static void plusY(this Transform transform, float y){
		Vector3 newPosition =   
			new Vector3(transform.position.x, transform.position.y + y, transform.position.z);  

		transform.position = newPosition;  
	}

	public static void plusZ(this Transform transform, float z){
		Vector3 newPosition =   
			new Vector3(transform.position.x, transform.position.y, transform.position.z+z);  

		transform.position = newPosition;  
	}

	public static void minusX(this Transform transform, float x){
		Vector3 newPosition =   
			new Vector3(transform.position.x - x, transform.position.y, transform.position.z);  

		transform.position = newPosition;  
	}

	public static void minusY(this Transform transform, float y){
		Vector3 newPosition =   
			new Vector3(transform.position.x, transform.position.y-y, transform.position.z);  

		transform.position = newPosition;  
	}

	public static void minusZ(this Transform transform, float z){
		Vector3 newPosition =   
			new Vector3(transform.position.x, transform.position.y, transform.position.z-z);  

		transform.position = newPosition;  
	}
}
