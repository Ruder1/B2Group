// drawing.component.ts
import { Component, ElementRef, ViewChild, AfterViewInit, OnInit } from '@angular/core';
import { RequestService } from '../service/requests.service';
import { Point, Polygon } from '../models/polygon.model';

@Component({
  selector: 'app-drawing',
  templateUrl: './drawing.component.html',
  styleUrls: ['./drawing.component.css'],
})
export class DrawingComponent implements AfterViewInit, OnInit {
  @ViewChild('myCanvas', { static: true }) canvasRef: ElementRef;
  context: CanvasRenderingContext2D;
  drawing = false;
  drawingPolygon = false;
  drawingPoint = false;
  polygon: Point[] = [];
  polygons: Polygon[] = [];
  point: Point ={x: 0,y: 0};
  isPointInsidePolygon: boolean = false;


  constructor(private requestService: RequestService) {
  }

  ngOnInit() {
    this.requestService.getPolygons().subscribe(
       (polygons) => {
        this.polygons = polygons;
        console.log(polygons) // обработка полученных полигонов
       },
       (error) => {
         console.error('Error fetching polygons', error);
       }
    );
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
    this.context.arc(x, y, 3, 0, 2 * Math.PI);
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
      (response) => {
        this.isPointInsidePolygon = Boolean(response);
        console.log('Is Point inside Polygon checked successfully', response)
      },
      (error) => console.error('Error checking is Point inside Polygon', error)
    );
  }

  savePolygon() {
    let name = this.generateRandomPolygonName();
    console.log(name)
    this.requestService.savePolygon(this.polygon,name).subscribe(
      (response:any) => console.log('Polygon saved successfully', response),
      (error) => console.error('Error saving polygon', error)
    );
  }

  generateRandomPolygonName(): string {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let result = '';
    const length = 10; // Длина случайного имени
    for (let i = 0; i < length; i++) {
       result += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return result;
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

  onPolygonSelect(event: Event) {
    const selectElement = event.target as HTMLSelectElement;
    const selectedPolygonId = selectElement.value;

    const selectedPolygon = this.polygons.find((polygon) => {
      return polygon.id === Number(selectedPolygonId);
    });

    console.log('Selected Polygon:', selectedPolygon);

    if (selectedPolygon) {
      this.drawSelectedPolygon(selectedPolygon);
    } else {
      console.log('Selected Polygon not found');
    }
  }

   drawSelectedPolygon(polygon: Polygon) {
    this.polygon = polygon.points
    this.context.clearRect(0, 0, this.canvasRef.nativeElement.width, this.canvasRef.nativeElement.height);
   
    this.context.beginPath();
   
    this.context.moveTo(polygon.points[0].x, polygon.points[0].y);
    for (let i = 1; i < polygon.points.length; i++) {
       this.context.lineTo(polygon.points[i].x, polygon.points[i].y);
    }
   
    this.context.closePath();
    this.context.stroke();
   }
}
