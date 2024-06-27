
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DualPantoToolkit
{
    /// <summary>
    /// Applies a force directed at the center of the field on any object with a "MeHandle" or "ItHandle" tag within its area.
    /// </summary>
    ///
    public class PIDForceField : ForceField
    {
        [Tooltip("Positive strength will push the handle towards the center, negative strength towards the edges")]
        [Range(-1, 1)]
        public float strength;
        public bool isAttractive = true; // can be attractive or repulsive

        protected override float GetCurrentStrength(Collider other)
        {
            float dist = (Vector3.Distance(gameObject.transform.position, other.transform.position));
            if (isAttractive)
            {
                return strength * dist;
            } else
            {
                //repulsive center force field
                float radius = transform.localScale.x / 2;
                return (radius - dist) * strength;
            }
        }

        protected override Vector3 GetCurrentForce(Collider other)
        {
            // try not to oscillate in the center
            if (Vector3.Distance(gameObject.transform.position, other.transform.position) < 0.4 && isAttractive) return Vector3.zero;
            if (isAttractive)
            {
                return (gameObject.transform.position - other.transform.position).normalized;
            } else
            {
                return -(gameObject.transform.position - other.transform.position).normalized;
            }
        }
    }
}