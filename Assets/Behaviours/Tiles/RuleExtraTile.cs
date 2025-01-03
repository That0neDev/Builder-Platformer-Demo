using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Behaviours.Tiles{
    [CreateAssetMenu(menuName ="Custom/ExtraTile")]
    public class RuleExtraTile : RuleTile<RuleExtraTile.Neighbor>
    {
        public TileMaterial material;
        //public List<TileBase> friendlyTiles;

        public class Neighbor : TilingRuleOutput.Neighbor
        {
            //public const int Any = 3;
        }

        public override bool RuleMatch(int neighbor, TileBase tile)
        {
            switch (neighbor)
            {
                case Neighbor.This: return tile != null;
                case Neighbor.NotThis: return tile == null;
                //case Neighbor.Any: return MatchAny(tile);
            }

            return base.RuleMatch(neighbor, tile);
        }

        private bool MatchAny(TileBase tile) 
        {
            return tile != null;
        }
    }
}