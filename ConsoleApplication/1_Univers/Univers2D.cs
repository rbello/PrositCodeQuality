using ConsoleApplication.Elements;
using System.Collections.Generic;
using System;
using System.Windows;
using ConsoleApplication.Annotations;
using ConsoleApplication.Moves;

namespace ConsoleApplication.Univers
{
    public class Univers2D : IUnivers
    {

        private List<IElement> _list = new List<IElement>();

        public event ElementAddedEventHandler OnElementAdded;
        public event ElementRemovedEventHandler OnElementRemoved;
        public event ElementsCollisionEventHandler OnCollision;

        public IEnumerable<IElement> Elements
        {
            get
            {
                return _list;
            }
        }

        public int ElementsCount
        {
            get
            {
                return _list.Count;
            }
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        [SucreSyntaxique]
        public IElement this[Point location]
        {
            get
            {
                // On cherche l'élément qui est à cet emplacement
                foreach (IElement el in _list)
                {
                    if (el.Location == location) return el;
                }
                return null;
            }
            set
            {
                // On modifie l'emplacement d'un élément en passant par l'univers
                value.Location = location;
            }
        }

        [SucreSyntaxique]
        public IElement this[int index]
        {
            get
            {
                return _list[index];
            }
        }

        public Univers2D(int width, int height)
        {
            Width = width;
            Height = height;
        }

        [SucreSyntaxique]
        public static Univers2D operator +(Univers2D c1, IElement c2)
        {
            c1.Add(c2);
            return c1;
        }

        public void Add(IElement element)
        {
            // Cet élément existe déjà dans l'univers
            if (_list.Contains(element))
            {
                throw new ArgumentException("Element allready exists in this univers");
            }
            // On associe l'élément à l'univers
            element.Univers = this;
            // Par cette manipulation on fait revérifier l'emplacement par le setter
            // en prenant en compte l'univers
            element.Location = element.Location;
            // Et on l'ajoute dans l'univers
            _list.Add(element);
            // Et on lève un event
            if (OnElementRemoved != null)
            {
                OnElementRemoved(new ElementRemovedEvent(this, element));
            }
        }

        public void Remove(IElement element)
        {
            // On désassocie l'univers de l'élement
            if (element.Univers == this)
            {
                element.Univers = null;
            }
            // Et on le retire de l'univers
            if (_list.Contains(element))
            {
                _list.Remove(element);
                // Event
                if (OnElementRemoved != null)
                {
                    OnElementRemoved(new ElementRemovedEvent(this, element));
                }
            }
        }

        public bool IsInside(Point point)
        {
            return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
        }

        public void Update()
        {
            // On met à jour la position des éléments
            _list.ForEach((element) => element.UpdateLocation());
        }

        public void ThrowCollisionEvent(ElementsCollisionEvent evt)
        {
            if (OnCollision != null)
            {
                OnCollision(evt);
            }
        }
    }
}
