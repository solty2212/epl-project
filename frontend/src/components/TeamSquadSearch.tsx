import { useState } from "react";
import { TextField, Button, Container, List, ListItem, ListItemAvatar, Avatar, ListItemText, Typography, CircularProgress } from "@mui/material";
import SearchIcon from "@mui/icons-material/Search";
import { Player } from "../interfaces/player";

const API_URL = import.meta.env.VITE_API_URL;

const fetchTeamData = async (teamName: string) => {
    if (!teamName) return [];
    try {
        const response = await fetch(`${API_URL}/Team/squad?teamName=${teamName}`);
        const data = await response.json();

        return data || [];
    } catch (error) {
        console.error("Error fetching team data:", error);
        return [];
    }
};

const TeamSquadSearch = () => {
    const [teamName, setTeamName] = useState("");
    const [players, setPlayers] = useState<Array<Player>>([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const handleSearch = async () => {
        if (!teamName.trim()) {
            setError("Please enter a team name.");
            return;
        }
        setError("");
        setLoading(true);
        const data = await fetchTeamData(teamName);
        setPlayers(data);
        setLoading(false);
    };

    return (
        <Container maxWidth="sm" sx={{ mt: 5 }}>
            <Typography variant="h4" gutterBottom>Search Premier League Team</Typography>
            <TextField
                fullWidth
                label="Enter Team Name"
                variant="outlined"
                value={teamName}
                onChange={(e) => setTeamName(e.target.value)}
                sx={{ mb: 2 }}
                error={!!error}
                helperText={error}
            />
            <Button
                variant="contained"
                startIcon={<SearchIcon />}
                onClick={handleSearch}
                disabled={loading}
            >
                {loading ? <CircularProgress size={24} /> : "Search"}
            </Button>
            {players.length === 0 && !loading && !error && (
                <Typography variant="h6" sx={{ mt: 3, color: "gray" }}>No players found. Try another team.</Typography>
            )}
            <List sx={{ mt: 3 }}>
                {players.map((player, index) => (
                    <ListItem key={index}>
                        <ListItemAvatar>
                            <Avatar src={player.profilePicture} alt={player.fullName} />
                        </ListItemAvatar>
                        <ListItemText primary={`${player.fullName} - ${player.dateOfBirth}`} secondary={player.position} />
                    </ListItem>
                ))}
            </List>
        </Container>
    );
};

export default TeamSquadSearch;
