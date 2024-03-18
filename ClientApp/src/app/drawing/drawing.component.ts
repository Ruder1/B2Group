// drawing.component.ts
import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';

@Component({
 selector: 'app-drawing',
 templateUrl: './drawing.component.html',
 styleUrls: ['./drawing.component.css']
})
export class DrawingComponent implements AfterViewInit {
 @ViewChild('myCanvas', { static: true }) canvasRef: ElementRef;
 context: CanvasRenderingContext2D;
 drawing = false;
 points:{x:number;y:number}[] = [];

 constructor() {}

 ngAfterViewInit() {
    const canvas = this.canvasRef.nativeElement;
    this.context = canvas.getContext('2d');

    canvas.onmousedown = (e:MouseEvent) => {
      this.drawing = true;
      this.points.push({ x: e.clientX - canvas.offsetLeft, y: e.clientY - canvas.offsetTop });
    };

    canvas.onmousemove = (e:MouseEvent) => {
      if (!this.drawing) return;
      this.points.push({ x: e.clientX - canvas.offsetLeft, y: e.clientY - canvas.offsetTop });
      this.drawPolygon();
    };

    canvas.onmouseup = () => {
      this.drawing = false;
    };
 }

 drawPolygon() {
    this.context.clearRect(0, 0, this.canvasRef.nativeElement.width, this.canvasRef.nativeElement.height);
    this.context.beginPath();
    this.context.moveTo(this.points[0].x, this.points[0].y);

    for (let i = 1; i < this.points.length; i++) {
      this.context.lineTo(this.points[i].x, this.points[i].y);
      console.log(this.points[i].x,this.points[i].y)
    }
    this.context.closePath();
    this.context.stroke();
 }
}