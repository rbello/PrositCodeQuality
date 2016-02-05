using System;
using System.Windows;
using ConsoleApplication.Moves;
using ConsoleApplication.Univers;

namespace ConsoleApplication.Elements
{
    public abstract class AElement : IElement
    {

        public AElement(string name, string symbol, double mass)
        {
            Name = name;
            Symbol = symbol;
            Mass = mass;
            Location = new Point(0, 0);
        }

        public virtual string Name { get; protected set; }

        public virtual string Symbol { get; protected set; }

        public virtual double Mass { get; protected set; }

        private Point _location;

        public event ElementMovedEventHandler OnMoved;

        public Point Location
        {
            get
            {
                return _location;
            }
            set
            {
                // Si cet élément est relié à un univers, on vérifie la position
                if (Univers != null)
                {
                    // On vérifie qu'il n'y ait rien à cet emplacement
                    if (Univers[value] != null/* && Univers[value] != this*/)
                    {
                        throw new ElementCollisionException(String.Format("Location [{0};{1}] is allready occuped", value.X, value.Y));
                    }
                    // Et qu'on ne soit pas en dehors des bornes
                    if (!Univers.IsInside(value))
                    {
                        throw new ArgumentOutOfRangeException(String.Format("Location [{0};{1}] is out of univers' range", value.X, value.Y));
                    }
                }
                // On ne modifie pas l'emplacement
                if (value == Location) return;
                // On modifie l'emplacement
                Point old = _location;
                _location = value;
                // Event
                if (OnMoved != null)
                {
                    OnMoved(new ElementMovedEvent(this, old, value));
                }
            }
        }

        public IUnivers Univers { get; set; }

        public IMoveStrategy MoveStrategy { get; set; }

        public Vector Displacement { get; set; }

        public void UpdateLocation()
        {
            // On délègue à la stratégie
            if (MoveStrategy != null)
            {
                MoveStrategy.ExecuteMove(this);
            }
        }
    }
}
