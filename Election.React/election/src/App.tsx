import './App.css';
import Elections from './pages/elections/elections';
import Election from './pages/electionDetails/election';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return(
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Elections />} />
        <Route path="/elections" element={<Elections />} />
        <Route path="/elections/:id" element={<Election />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App;