//===========================================================================================================
//===========================================================================================================
// Structure de données



precision mediump float;
varying vec3 rayDir;

struct Rayon 
{
	vec3 o,v;
	float t;
};

struct Brdf
{
	vec3 kd;
	int n;
	float ks;
};


struct Sphere
{
	vec3 c;
	float r;
	Brdf properties;
};

struct Source
{
	vec3 puissance, pos;
};


Rayon ray;
Sphere tableau[2],sph;
Source source;
float T_min, cos_alpha, cos_theta;
vec3 I, normal, v0, vi, h, f, li, l0;




//===========================================================================================================
//===========================================================================================================
// Methode



float intersectSphere (inout Rayon r, Sphere s)
{
	vec3 Centre = r.o-s.c;
	
	float C = dot(Centre, Centre) - s.r*s.r;
	float B = dot(Centre, r.v) * 2.0;
	float A = dot(r.v, r.v);
	
	float D = B*B - 4.0*A*C;
	
	if (D < 0.0) 
	{
		return -1.0;
	}
	else {
		float T1 = (-B-sqrt(D))/(2.0*A);
		float T2 = (-B+sqrt(D))/(2.0*A);
		
		if (T1 < T2 && T1 > 0.0)
		{
			return T1;
		}
		else if (T2 < T1 && T2 > 0.0)
		{
			return T2;
		}
		else
		{
			return -1.0;
		}
	}

}

//==========================================================================================================

Sphere choixSphere(Sphere tableau[2], Rayon r, out float T)
{
	T = -1.0;
	float T_temp;
	Sphere Si;
	Si.r = -1.0;
	
	
	for (int i=0; i<2; i++)
	{
		T_temp = intersectSphere(r, tableau[i]);
		if (T_temp >= 0.0)
		{
			if(T_temp < T || T < 0.0)
			{
				Si = tableau[i];
				T = T_temp;		
			}
		}	
	}	
	return Si;
}


//===========================================================================================================
//===========================================================================================================
// Main : Disque


void main(void) {
	

	tableau[0] = Sphere(vec3(0.0, 200.0, 0.0), 20.0, Brdf(vec3(1.0,0.0,0.0), 1, 1.0)); //Sphere rouge
	tableau[1] = Sphere(vec3(10.0, 200.0, 0.0), 20.0, Brdf(vec3(0.0,1.0,0.0), 1, 1.0)); // Sphere verte
	
	source = Source(vec3(2.0,2.0,2.0), vec3(0.0,200.0,300.0));

	ray.o = vec3(0.0,0.0,0.0);
	ray.v = rayDir;
		
	sph = choixSphere(tableau, ray, T_min); // Choisi la sphère la plus proche
	
	
	
	if (sph.r != -1.0)
	{
		I = ray.v * T_min;
		normal = normalize(I - sph.c);
		v0 = normalize(-ray.v);
		vi = normalize(source.pos-I);
		h = normalize(vi+v0);
		cos_alpha = dot(normal, h);
		f = (sph.properties.kd / 3.14)+(sph.properties.ks*((float(sph.properties.n +8))/(8.0*3.14))) * pow(cos_alpha, float(sph.properties.n));
		cos_theta = dot(normal, vi);
		li = source.puissance;
		l0 = li * f * cos_theta;
		gl_FragColor = vec4(l0,1.0);	
	}
	
	else 
	{
		gl_FragColor = vec4(0.0,0.0,0.0,1.0);		
	}

}
