// drawing.component.ts
import { Component, ElementRef, ViewChild, AfterViewInit } from '@angular/core';
import { RequestService } from '../service/requests.service';

@Component({
  selector: 'app-drawing',
  templateUrl: './drawing.component.html',
  styleUrls: ['./drawing.component.css'],
})
export class DrawingComponent implements AfterViewInit {
  @ViewChild('myCanvas', { static: true }) canvasRef: ElementRef;
  context: CanvasRenderingContext2D;
  drawing = false;
  drawingPolygon = false;
  drawingPoint = false;
  polygon: { x: number; y: number }[] = [];
  point: { x: number; y: number} ={
    x: 0,
    y: 0
  };

  constructor(private requestService: RequestService) {
  }

  ngAfterViewInit() {
    const canvas = this.canvasRef.nativeElement;
    this.context = canvas.getContext('2d');
    
    canvas.onmousedown = (e: MouseEvent) => {
      if (this.drawingPolygon) {
        this.polygon.push({
          x: e.clientX - canvas.offsetLeft,
          y: e.clientY - canvas.offsetTop,
        });
        this.drawing = true;
      } 
      else if (this.drawingPoint) {
        this.drawPoint(
          e.clientX - canvas.offsetLeft,
          e.clientY - canvas.offsetTop
        );
      }
    };

    canvas.onmousemove = (e: MouseEvent) => {
      if (!this.drawing) return;
      if (this.drawingPolygon) {
        this.polygon.push({
          x: e.clientX - canvas.offsetLeft,
          y: e.clientY - canvas.offsetTop,
        });
        this.drawPolygon();
      }
    };

    canvas.onmouseup = () => {
      this.drawing = false;
    };
  }

  drawPolygon() {
    this.context.clearRect(
      0,
      0,
      this.canvasRef.nativeElement.width,
      this.canvasRef.nativeElement.height
    );
    this.context.beginPath();
    this.context.moveTo(this.polygon[0].x, this.polygon[0].y);

    for (let i = 1; i < this.polygon.length; i++) {
      this.context.lineTo(this.polygon[i].x, this.polygon[i].y);
    }
    this.context.closePath();
    this.context.stroke();
  }

  drawPoint(x: number, y: number) {
    console.log(x, y);
    this.context.clearRect(this.point.x - 5, this.point.y - 5, 8, 8);
    this.point = { x: x, y: y };
    this.context.beginPath();
    this.context.arc(x, y, 3, 0, 2 * Math.PI); // Рисуем круг радиусом 5 пикселей
    this.context.fillStyle = 'red';
    this.context.fill();
  }

  clearCanvas() {
    this.context.clearRect(
      0,
      0,
      this.canvasRef.nativeElement.width,
      this.canvasRef.nativeElement.height
    );
    this.polygon = []; // Очищаем массив точек
    this.point = { x: 0, y: 0 };
  }

  checkPointInsidePolygon() {
    console.log(this.polygon);
    console.log(this.point)

    this.requestService.checkPointInsidePolygon(this.polygon,this.point).subscribe(
      (response) => console.log('Is Point inside Polygon checked successfully', response),
      (error) => console.error('Error checking is Point inside Polygon', error)
    );
  }

  savePolygon() {
    this.requestService.savePolygon(this.polygon).subscribe(
      (response) => console.log('Polygon saved successfully', response),
      (error) => console.error('Error saving polygon', error)
    );
  }

  startDrawingPolygon() {
    this.drawingPolygon = true;
    this.drawingPoint = false;
  }

  startDrawingPoint() {
    console.log(1);
    this.drawingPoint = true;
    this.drawingPolygon = false;
  }
}
