using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FlipperDunkClone.Managers;

namespace FlipperDunkClone.Controllers
{
	public class PlayerController : MonoBehaviour
	{
		GameSettingsManager gameSettingManager;

		private Rigidbody2D _rigitbody;

		void Start()
		{

			_rigitbody = GetComponent<Rigidbody2D>();
		}

		void Update()
		{
			if (Input.GetMouseButton(0))
			{
				_rigitbody.velocity = new Vector2(_rigitbody.velocity.x, GameSettingsManager.Instance.gameSettings.jumpForce);
			}

			if (_rigitbody.velocity.y < GameSettingsManager.Instance.gameSettings.maxFallSpeed)
			{

				_rigitbody.velocity += Vector2.up * Physics2D.gravity.y * GameSettingsManager.Instance.gameSettings.fallSpeed * Time.deltaTime;
			}

		}

	}
}

