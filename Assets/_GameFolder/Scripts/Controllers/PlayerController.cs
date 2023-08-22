using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FlipperDunkClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		public float jumpForce;
		public float fallSpeed;
		public float maxFallSpeed;

		private Rigidbody2D _rigitbody;

		void Start()
		{

			_rigitbody = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				_rigitbody.velocity = new Vector2(_rigitbody.velocity.x, jumpForce);
			}

			if (_rigitbody.velocity.y < maxFallSpeed)
			{

				_rigitbody.velocity += Vector2.up * Physics2D.gravity.y * fallSpeed * Time.deltaTime;
			}
		
			if (Input.GetKeyDown(KeyCode.Space))
			{
				Debug.Log(_rigitbody.velocity.y);
				Debug.Log(maxFallSpeed);
			}
		}

	}
}

