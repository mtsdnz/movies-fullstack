import { Outlet } from 'react-router-dom';
import { AppNavbar } from '../components/navbar';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';
import Row from 'react-bootstrap/Row';

export function RootLayout() {
  return (
    <div className="root-layout">
      <header>
        <AppNavbar />
      </header>
      <main className="d-flex flex-column h-100">
        <Container>
          <Outlet />
        </Container>
      </main>
      <footer className="footer mt-auto py-3 bg-light">
        <Container>
          <Row>
            <Col className="text-center py-3">&copy; 2024 Matheus Diniz</Col>
          </Row>
        </Container>
      </footer>
    </div>
  );
}
